using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Xml.Linq;
using Facebook;
using Panda_20.gui;
using Panda_20.model;
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

        private static String _facebookToken;

        public static String FacebookToken
        {
            get
            {
                return _facebookToken;
            }
        }

        // Her gemmes Token og en Unix Time for hvornår det token udløber i et string array.

        // Ovenstående kommentar forklarer det vist. Bemærk dog, at jeg hele tiden har haft XML'en
        // in mente. Derfor er string[]'et bare en temp løsning for at få sendt dataene videre fra GUI-laget. -Frede
        private static string[] tokenAndExpiresIn = new string[2];

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

        //private Service()
        //{
        //    TokenAndExpiresIn = new string[2];
        //    _pages = new Dictionary<string, JsonObject>();
        //    _pagePictures = new Dictionary<string, string>();
        //}

        //public static Service Instance
        //{
        //    get
        //    {
        //        return ServiceInstance;
        //    }
        //}

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

        public static string[] TokenAndExpiresIn
        {
            get { return tokenAndExpiresIn; }
            set { tokenAndExpiresIn = value; }
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
        //--------------<READ XML VALUE>---------------- Author: TRR 
        //-----------------------------------------------------------
        public static String GetXmlElement(String elementName)
        {

            XDocument document = XDocument.Load(@"service\AppValues.xml");
            XElement element = document.Root.Element(elementName);

            if (element == null)
            {
                throw new Exception("Element not found in XML-file!");
            }

            return element.Value;
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
            string accessToken = TokenAndExpiresIn[0];
            SetFacebookToken(accessToken);

            JsonObject response = _loginClient.Get("me/accounts") as JsonObject;

            if (_pages.Count == 0)
            {
                foreach (var account in (JsonArray) response["data"])
                {
                    JsonObject jsonAccount = ((JsonObject) account);
                    string name = (string) jsonAccount["name"];
                    _pages.Add(name, jsonAccount);

                    // TODO picUrl kan omnavngives for bedre forståelse. Se getPictureURL(..)

                    string picUrl = ("http://graph.facebook.com/" + (string) jsonAccount["id"]) + "/picture?redirect=false";
                    _pagePictures.Add((string) jsonAccount["name"], Misc.GetPagePictureUrl(picUrl));
                }
            }
        }

        public static void CreateNotification(PandaNotification pn)
        {
            int userFriends = Convert.ToInt32(pn.Owner.FriendCount) + Convert.ToInt32(pn.Owner.SubscriberCount);
            NotificationPopup np = new NotificationPopup(pn.Message, pn.Owner.Name, pn.Owner.PicSquare, pn.GetType().ToString(), Convert.ToString(userFriends), pn.Nid);
            np.Show();
        }
    }
}
