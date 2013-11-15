﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using Facebook;


namespace Panda_20
{

    /// <summary>
    /// Service-klasse der tillader adgang mellem GUI og model-lag jævnført Model/View/Controller-modellen.
    /// </summary>
    public sealed class Service
    {

        private static readonly Service ServiceInstance = new Service();

        // TODO Redundans; vi skal bruge XML'en alligevel
        private const String AppID = "470029853116845";
        private const String AppSecret = "5a62c1030284cbe12d06c79934fc7aea";
        private string GrantType { get; set; }

        private String _facebookToken;

        public String FacebookToken
        {
            get
            {
                return _facebookToken;
            }
        }

        // Her gemmes Token og en Unix Time for hvornår det token udløber i et string array.

        // Ovenstående kommentar forklarer det vist. Bemærk dog, at jeg hele tiden har haft XML'en
        // in mente. Derfor er string[]'et bare en temp løsning for at få sendt dataene videre fra GUI-laget. -Frede
        public string[] TokenAndExpiresIn { get; set; }
        private Dictionary<string, JsonObject> _pages;

        public Dictionary<string, JsonObject> Pages
        {
            get
            {
                return _pages;
            }
        }

        private Dictionary<string, string> _pagePictures;

        public Dictionary<string, string> PagePictures
        {
            get
            {
                return _pagePictures;
            }
        }

        private FacebookClient _client;
        private FacebookClient _pageClient;
        public JsonObject SelectedPage { get; set; }

        private Service()
        {
            TokenAndExpiresIn = new string[2];
            _pages = new Dictionary<string, JsonObject>();
            _pagePictures = new Dictionary<string, string>();
        }

        public static Service Instance
        {
            get
            {
                return ServiceInstance;
            }
        }

        //-----------------------------------------------------------
        //--------------<Set Page Access Token>---------Author: HJTH 
        //-----------------------------------------------------------
        // This FacebookClient is needed to get posts, private messages
        // and more from the selected page

        public FacebookClient SetPageFacebookClient(string pageAccessToken)
        {
            try
            {
                FacebookClient pageFacebookClient = new FacebookClient(pageAccessToken);
                this._pageClient = pageFacebookClient;
            }
            catch (FacebookOAuthException)
            {
                //TODO Håndter fejl
            }
            return this._pageClient;
        }

        //-----------------------------------------------------------
        //--------------<Get Long Lived AccessToken>----Author: HJTH 
        //-----------------------------------------------------------
        // This method might not be necessary as the app is developed 
        //as an offline app (selected on Facebook), but I will leave 
        // it here for now.

        public String GetLongLivedAccessToken(string shortLivedAccessToken)
        {
            JsonObject appData = new JsonObject();

            appData.Add("grant_type", "fb_exchange_token");
            appData.Add("client_id", AppID);
            appData.Add("client_secret", AppSecret);
            appData.Add("fb_exchange_token", shortLivedAccessToken);

            JsonObject result = (JsonObject) _client.Get("/oauth/access_token", appData);

            string extendedToken = (string) result["access_token"];

            return extendedToken;
        }

        //-----------------------------------------------------------
        //--------------<READ XML VALUE>---------------- Author: TRR 
        //-----------------------------------------------------------
        public String GetXmlElement(String elementName)
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

        public FacebookClient SetFacebookToken(String accessToken)
        {
            try
            {
                this._facebookToken = accessToken;
                FacebookClient newClient = new FacebookClient(accessToken);
                this._client = newClient;
            }
            catch (FacebookOAuthException)
            {
                //TODO Her skal det håndteres, hvis brugeren har valgt "nej" til at give os rettigheder,
                // eller hvis token'en er expired.
                //... basically, brugeren skal redirectes til facebook og logge ind igen. 
                // Eller skal vi bare lade exception'en blive kastet, og så fange den højere oppe? Hm...

            }

            return this._client;

        }

        //-----------------------------------------------------------
        //----------<FETCH USER'S PAGES>---------------- Author: FOL 
        //-----------------------------------------------------------

        public void FetchPages()
        {
            // Jeg kan ikke benytte _client.AccessToken i metodekaldet
            // nedenfor, da _client er null på det tidspunkt. Men den
            // bliver selvfølgelig brugt efterfølgende. -Frede
            string accessToken = TokenAndExpiresIn[0];
            SetFacebookToken(accessToken);

            JsonObject response = _client.Get("me/accounts") as JsonObject;

            if (_pages.Count == 0)
            {
                foreach (var account in (JsonArray) response["data"])
                {
                    JsonObject jsonAccount = ((JsonObject) account);
                    string name = (string) jsonAccount["name"];
                    _pages.Add(name, jsonAccount);

                    string picUrl = ("http://graph.facebook.com/" + (string) jsonAccount["id"]) + "/picture";
                    _pagePictures.Add((string) jsonAccount["name"], picUrl);
                }
            }
        }
    }
}
