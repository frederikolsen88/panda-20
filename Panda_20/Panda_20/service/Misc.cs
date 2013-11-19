using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace Panda_20.service
{
    class Misc
    {

        private static WebClient _webClient;

        // Accepts seconds to add or subtract
        public static long UnixTimeNow(long seconds)
        {
            TimeSpan _TimeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)_TimeSpan.TotalSeconds+seconds;
        }

        public static BitmapImage DownloadImage(string fullUrl)
        {
            Uri urlAsUri;
            bool urlIsValid = Uri.TryCreate(fullUrl, UriKind.Absolute, out urlAsUri);

            if (urlIsValid)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = urlAsUri;
                bitmap.EndInit();

                return bitmap;
            }

            else
            {
                throw new UriFormatException(fullUrl + " could not be parsed as an Uri!");
            }
        }

        public static string GetPagePictureUrl(string req)
        {
            string pictureUrl = "";

            if (_webClient == null) 
                _webClient = new WebClient();

            try
            {
                string response = _webClient.DownloadString(req);

                // Jeg har haft problemer med at arbejde med Json'en, der kommer tilbage.
                // Ergo denne workaround.

                pictureUrl = response.Split('"')[5].Replace(@"\", "");
            }

            catch (WebException)
            {
                pictureUrl = @"\resources\placeholder.gif";
            }

            return pictureUrl;
        }

        public static void DisposeWebClient()
        {
            if (_webClient != null)
                _webClient.Dispose();
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

        /**
         * Skriv til AppValues.xml
         */
        public static bool WriteXmlElement(string destination, string name, string value)
        {
            XDocument document = XDocument.Load(@"service\AppValues.xml");
            XElement newElement = new XElement(name, value);
            bool result = false;

            XElement destinationElement = document.Element(destination);

            if (destinationElement != null)
            {
                destinationElement.Add(newElement);
                document.Save(@"service\AppValues.xml");
                result = true;
            }
                
            return result;
        }
    }
}