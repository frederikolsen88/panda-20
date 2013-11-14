using System;
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

        public string[] TokenAndExpiresIn { get; set; }
        private ObservableCollection<string> pages;
        private FacebookClient client;

        private Service()
        {
            TokenAndExpiresIn = new string[2];
            pages = new ObservableCollection<string>();
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
            return client ?? (client = new FacebookClient(token));
        }

        //-----------------------------------------------------------
        //----------<FETCH USER'S PAGES>---------------- Author: FOL 
        //-----------------------------------------------------------

        public ObservableCollection<string> GetPages()
        {
            JsonObject response = GetClient(TokenAndExpiresIn[0]).Get("me/accounts") as JsonObject;

            if (pages.Count == 0)
            {
                foreach (var account in (JsonArray) response["data"])
                {
                    string name = (string)(((JsonObject)account)["name"]);
                    pages.Add(name);
                }
            }

            return pages;
        } 
    }
}
