using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Facebook;
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

        // TODO Redundans; vi skal bruge XML'en alligevel
        private const String AppID = "470029853116845";
        private const String AppSecret = "5a62c1030284cbe12d06c79934fc7aea";
        private static string GrantType { get; set; }
        private static Object XmlWriteLock = new Object();

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
        private static long lastSuccessfullFacebookUpdate;

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

        //-----------------------------------------------------------
        //--------------<Set Page Access Token>---------Author: HJTH 
        //-----------------------------------------------------------
        // This FacebookClient is needed to get posts, private messages
        // and more from the selected page

        public static FacebookClient SetPageFacebookClient(string pageAccessToken)
        {
            try
            {
                Console.WriteLine(SelectedPage["id"]);
                Console.WriteLine("page access token: " + pageAccessToken);
                FacebookClient pageFacebookClient = new FacebookClient(pageAccessToken);
                PageClient = pageFacebookClient;
                FBConnect.GetFacebookUpdates();
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

        public static String GetLongLivedAccessToken(string shortLivedAccessToken)
        {
            JsonObject appData = new JsonObject();

            appData.Add("grant_type", "fb_exchange_token");
            appData.Add("client_id", AppID);
            appData.Add("client_secret", AppSecret);
            appData.Add("fb_exchange_token", shortLivedAccessToken);

            JsonObject result = (JsonObject) _loginClient.Get("/oauth/access_token", appData);

            string extendedToken = (string) result["access_token"];

            return extendedToken;
        }

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

            JsonObject response = _loginClient.Get("me/accounts") as JsonObject;

            if (_pages.Count == 0)
            {
                foreach (var account in (JsonArray) response["data"])
                {
                    JsonObject jsonAccount = ((JsonObject) account);
                    string name = (string) jsonAccount["name"];
                    _pages.Add(name, jsonAccount);

                    string tempPicUrl = ("http://graph.facebook.com/" + (string) jsonAccount["id"]) + "/picture?redirect=false";
                    _pagePictures.Add((string) jsonAccount["name"], Misc.GetPagePictureUrl(tempPicUrl));
                }
            }
        }

        public static void CreateNotification(PandaNotification pn)
        {
            int userFriends = Convert.ToInt32(pn.Owner.FriendCount) + Convert.ToInt32(pn.Owner.SubscriberCount);
            NotificationPopup np = new NotificationPopup(pn.Message, pn.Owner.Name, pn.Owner.PicSquare, pn.GetType().ToString(), Convert.ToString(userFriends), pn.Nid);
            np.Show();
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


        //-------------------------------------------------------------------
        //--------------<GENERAL READ XML VALUE>---------------- Author: TRR 
        //-------------------------------------------------------------------
        
        // DEPRECATED - USE Settings.Default.NAMEOFYOURSETTING
        public static String ReadXmlValue(string elementName, string filePath)
        {   
            XDocument document = XDocument.Load(filePath);
            XElement element = document.Root.Element(elementName);

            if (element == null)
            {
                throw new Exception("Element <" + elementName + "> not found in XML-file <" + filePath +">.");
            }

            return element.Value;
        }

        //-------------------------------------------------------------------
        //--------------<GENERAL WRITE XML VALUE>--------------- Author: TRR 
        //-------------------------------------------------------------------
        // Note - ALL write-operations to XML-files should be done through
        // the use of this method, since it drastically lowers the odds for of write-operations
        // doing the dirty stuff at the same time. Won't happen often, but it would 
        // crash the program horribly if or when it happened.

        // DEPRECATED - USE:
        // Settings.Default.NAMEOFYOURSETTING = yourValue
        // Settings.Default.Save();
        // ...instead. See how in actual use in BrowserHelper-class.
       
        public static void WriteXmlValue(string elementName, string newValue, string filePath)
        {
            XDocument document = XDocument.Load(filePath);
            XElement retrievedElement = document.Root.Element(elementName);

            if (retrievedElement == null)
            {
                //All elements must exist in the XML-file prior to writing them 
                // - doing otherwise is dangerous.
                throw new Exception("No such XML element (" + elementName +") exists in the file <" + filePath + ">. Check your spelling - and add it manually if needed before trying to write to it");
            } 
            
            if (retrievedElement.Value.Equals(newValue))
            {
                //Don't initiate costly IO-operation if the value already is what it's supposed to be
            } else
            {
                //Locked so it's threadsafe to write to the file.
                lock (XmlWriteLock)
                {
                    retrievedElement.SetValue(newValue);
                    document.Save(@"service\AppValues.xml");
                }
            }
        }

        /**
         * Skriver en værdi til programmets settings, såfremt vi forsøger
         * at skrive til en property, der rent faktisk findes.
         */

        public static void WriteToConfig(string name, string value)
        {
            if (Settings.Default.Properties[name] != null)
            {
                Settings.Default[name] = value;
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
