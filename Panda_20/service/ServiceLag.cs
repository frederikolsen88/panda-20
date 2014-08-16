using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Media;
using System.Windows;
using System.Xml.Linq;
using Facebook;
using Microsoft.Win32;
using Panda_20.gui;
using Panda_20.model;
using Panda_20.Properties;
using Panda_20.service;


namespace Panda_20
{

    /// <summary>
    /// Service-klasse der tillader adgang mellem GUI og model-lag jævnført Model/View/Controller-modellen.
    /// </summary>

    public static class Service
    {
        //private const String AppID = "470029853116845";
        //private const String AppSecret = "5a62c1030284cbe12d06c79934fc7aea";
        private static string GrantType { get; set; }
        private static bool queueShown = false;
        private static SoundPlayer soundPlayer = new SoundPlayer(Properties.Resources.notify);

        private static String _facebookToken;

        public static String FacebookToken
        {
            get
            {
                return _facebookToken;
            }
        }

        private static Dictionary<string, JsonObject> _pages = new Dictionary<string, JsonObject>();

        public static Dictionary<string, JsonObject> Pages
        {
            get
            {
                return _pages;
            }
        }

        private static Dictionary<string, string> _pagePictures = new Dictionary<string, string>();

        public static Dictionary<string, string> PagePictures
        {
            get
            {
                return _pagePictures;
            }
        }

        private static FacebookClient _loginClient;
        private static FacebookClient _pageClient;

        public static JsonObject SelectedPage { get; set; }
        private static long lastSuccessfullFacebookUpdate = Misc.UnixTimeNow(0);

        public static long LastSuccessfullFacebookUpdate
        {
            get { return lastSuccessfullFacebookUpdate; }
            set { lastSuccessfullFacebookUpdate = value; }
        }

        public static FacebookClient PageClient
        {
            get { return _pageClient; }
            set { _pageClient = value; }
        }

        public static bool QueueShown
        {
            get { return queueShown; }
            set { queueShown = value; }
        }

        //-----------------------------------------------------------
        //--------------<Set Page Access Token>---------Author: HJTH 
        //-----------------------------------------------------------
        // This FacebookClient is needed to get posts, private messages
        // and more from the selected page
        [STAThread]
        public static FacebookClient SetPageFacebookClient(string pageAccessToken)
        {
            try
            {
                Console.WriteLine(SelectedPage["id"]);
                Console.WriteLine("page access token: " + pageAccessToken);
                FacebookClient pageFacebookClient = new FacebookClient(pageAccessToken);
                PageClient = pageFacebookClient;
                FbConnect.OneMinuteTimer();
            }
            catch (FacebookOAuthException)
            {
                //TODO Håndter fejl
            }
            return PageClient;
        }

        //-----------------------------------------------------------
        //--------------<Get Long Lived AccessToken>----Author: HJTH 
        //-----------------------------------------------------------
        // This method might not be necessary as the app is developed 
        //as an offline app (selected on Facebook), but I will leave 
        // it here for now.

        //public static String GetLongLivedAccessToken(string shortLivedAccessToken)
        //{
        //    JsonObject appData = new JsonObject();

        //    appData.Add("grant_type", "fb_exchange_token");
        //    appData.Add("client_id", AppID);
        //    appData.Add("client_secret", AppSecret);
        //    appData.Add("fb_exchange_token", shortLivedAccessToken);

        //    JsonObject result = (JsonObject) _loginClient.Get("/oauth/access_token", appData);

        //    string extendedToken = (string) result["access_token"];

        //    return extendedToken;
        //}

        //-----------------------------------------------------------
        //--------------<SET FACEBOOK TOKEN>--------------Author: TRR 
        //-----------------------------------------------------------
        // Givet accesstoken'en, sætter den og opdaterer FacebookClient
        // (og returner den)

        public static FacebookClient SetFacebookToken(String accessToken)
        {
            try
            {
                _facebookToken = accessToken;
                FacebookClient newClient = new FacebookClient(accessToken);
                _loginClient = newClient;
            }
            catch (FacebookOAuthException)
            {
                //TODO Her skal det håndteres, hvis brugeren har valgt "nej" til at give os rettigheder,
                // eller hvis token'en er expired.
                //... basically, brugeren skal redirectes til facebook og logge ind igen. 
                // Eller skal vi bare lade exception'en blive kastet, og så fange den højere oppe? Hm...

            }

            return _loginClient;

        }

        //-----------------------------------------------------------
        //----------<FETCH USER'S PAGES>---------------- Author: FOL 
        //-----------------------------------------------------------

        public static void FetchPages()
        {
            // Jeg kan ikke benytte _loginClient.AccessToken i metodekaldet
            // nedenfor, da _loginClient er null på det tidspunkt. Men den
            // bliver selvfølgelig brugt efterfølgende. -Frede
            
            SetFacebookToken(ReadFromConfig("fb_token"));

            try
            {
                JsonObject response = _loginClient.Get("me/accounts?fields=link,category,name,access_token,perms,id") as JsonObject;

                if (_pages.Count == 0)
                {
                    foreach (var account in (JsonArray) response["data"])
                    {
                        JsonObject jsonAccount = ((JsonObject) account);
                        string name = (string) jsonAccount["name"];
                        _pages.Add(name, jsonAccount);

                        string tempPicUrl = ("http://graph.facebook.com/" + (string) jsonAccount["id"]) +
                                            "/picture?redirect=false";
                        _pagePictures.Add((string) jsonAccount["name"], Misc.GetPagePictureUrl(tempPicUrl));
                    }
                }
            }

            catch (Facebook.WebExceptionWrapper)
            {
                TerminationAssistant.ShowErrorPopUp(null, "Panda was unable to connect to Facebook. Click OK to close the application.");
            }
            
        }

        
        public static void CreateNotification(PandaNotification pn)
        {
            Console.WriteLine("TYPE: " + pn.GetType().ToString());
            Console.WriteLine(soundPlayer.SoundLocation);

            if(Settings.Default.notifications_play_sound)
                soundPlayer.Play();

            if (Queue.DisplayedNotifications.Count < Convert.ToInt32(Service.ReadFromConfig("notifications_max_amount")))
            {
                NotificationPopup np;
                if (pn.GetType().ToString() == "Panda_20.model.PandaComment")
                {
                    PandaComment pc = (PandaComment)pn;
                    np = new NotificationPopup(pc);
                }
                else
                {
                    np = new NotificationPopup(pn);
                }

                Queue.AddDisplayedNotification(np);
                np.Show();
            }
            else
            {
                if (!Queue.QueueNotifications.Contains(pn))
                {
                    Queue.AddQueueNotification(pn);
                    if (!QueueShown)
                    {
                        Queue.Qp.updateQueueCount(Convert.ToString(Queue.QueueNotifications.Count));
                        Queue.Qp.Show();
                        QueueShown = true;
                    }
                    else
                    {
                        Queue.Qp.updateQueueCount(Convert.ToString(Queue.QueueNotifications.Count));
                    }
                }
            }
            
        }

        //-----------------------------------------------------------
        //--------------<RETURN PAGE LIKES>---------------Author: FOL 
        //-----------------------------------------------------------
        public static Int32 GetLikes(string id)
        {
            Int32 likes = 0;

            JsonObject response = _loginClient.Get("//" + id + "?fields=likes") as JsonObject;

            if (response != null)
            {
                object likesValue;
                if (response.TryGetValue("likes", out likesValue))
                    likes = Convert.ToInt32(likesValue);
            }

            return likes;
        }

        /// <summary>
        /// Method that adds (isSupposedToStartWithWindoes == true) or removes ( == false) a registry value to the RUN registry (in the HKEY_CURRENT_USRE/SOFTWARE/Microsoft/Windows/CurrentVersion/Run
        /// </summary>
        /// <param name="isSupposedToStartWithWindows"></param>
        public static void ConfigureRegistryKeyForStartup(bool isSupposedToStartWithWindows)
        {

            string appName = Settings.Default.RegistryValueName;

            RegistryKey runKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (runKey == null)
            {
                throw new Exception("Run key somehow does not exist in the registry. This cannot happen... Congratulations, you basically broke Windows.");
            }

            String value = (String) runKey.GetValue(appName, null);

            if (isSupposedToStartWithWindows) //supposed to start and key doesn't exist (in other words, need to create the key)
            {

                String exePath = "\"" + System.Reflection.Assembly.GetExecutingAssembly().Location + "\"";
                runKey.SetValue(appName, exePath);
                Settings.Default.start_with_windows = true;
                Settings.Default.Save();

            }
            else // not supposed to start (in other words, needs to delete the key)
            {
                runKey.DeleteValue(appName, false);
                Settings.Default.start_with_windows = false;
                Settings.Default.Save();
            } 
        }

        /**
         * Henter værdien af en given property i settings, såfremt
         * den findes. Bemærk, at vi kun returnere som en string.
         * Dér hvor man skal bruge setting'en, skal der castes afhg.
         * af forventet indhold.
         */

        public static string ReadFromConfig(string name)
        {
            string setting = "";

            if (Settings.Default.Properties[name] != null)
                setting = Settings.Default[name].ToString();

            return setting;
        }
    }
}
