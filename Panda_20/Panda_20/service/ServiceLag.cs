using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
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

        private Service()
        {
            FacebookClient fbClient = new FacebookClient();
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
            XDocument document = XDocument.Load(@".\service\AppValues.xml");
            XElement element = document.Root.Element("values").Element(elementName);

            if (element == null)
            {
                throw new Exception("Element not found in XML-file!");
            } 
                
            return element.Value;
        }
    }

}
