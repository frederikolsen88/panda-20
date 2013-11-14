using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Xml.Linq;
using Facebook;


namespace Panda_20
{

    /// <summary>
    /// Service-klasse der tillader adgang mellem GUI og model-lag jævnført Model/View/Controller-modellen.
    /// </summary>
    public sealed class Service
    {
        //-----------------------------------------------------------
        //---------------<SINGLETON>-------------------- Author: TRR
        //-----------------------------------------------------------

        private static readonly Service ServiceInstance = new Service();

        // TODO Redundans; vi skal bruge XML'en alligevel
        public string[] TokenAndExpiresIn { get; set; }
        private readonly Dictionary<string, JsonObject> _pages;
        private FacebookClient _client;

        public JsonObject SelectedPage { get; set; }

        private Service()
        {
            TokenAndExpiresIn = new string[2];
            _pages = new Dictionary<string, JsonObject>();
        } 

        public static Service Instance
        {
            get
            {
                return ServiceInstance;
            }
        } 

        //-----------------------------------------------------------
        //--------------<READ XML VALUE>---------------- Author: TRR 
        //-----------------------------------------------------------

        public String GetXmlElement(String elementName)
        {

            XDocument document = XDocument.Load(@"service\AppValues.xml");
            XElement element = document.Element("values").Element(elementName);

            if (element == null)
            {
                throw new Exception("Element not found in XML-file!");
            }

            return element.Value;
        }

        //-----------------------------------------------------------
        //--------------<GET FB CLIENT>----------------- Author: FOL 
        //-----------------------------------------------------------

        private FacebookClient GetClient(string token)
        {
            return _client ?? (_client = new FacebookClient(token));
        }

        //-----------------------------------------------------------
        //----------<FETCH USER'S PAGES>---------------- Author: FOL 
        //-----------------------------------------------------------

        public Dictionary<string, JsonObject> GetPages()
        {
            JsonObject response = GetClient(TokenAndExpiresIn[0]).Get("me/accounts") as JsonObject;

            if (_pages.Count == 0)
            {
                foreach (var account in (JsonArray) response["data"])
                {
                    JsonObject jsonAccount = ((JsonObject)account);
                    string name = (string) jsonAccount["name"];
                    _pages.Add(name, jsonAccount);
                }
            }

            return _pages;
        } 
    }
}
