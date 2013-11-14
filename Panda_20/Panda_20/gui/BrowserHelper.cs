using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Panda_20.gui
{
    /// <summary>
    /// Hjælpeklasse til FB login browser.
    /// 
    /// Author: Frederik Olsen
    /// </summary>
    class BrowserHelper
    {

        public static Uri CurrentUri { get; set; }

        public static void InitBrowser(WebBrowser browser)
        {
            CurrentUri = new Uri(Service.Instance.GetXmlElement("fbUrl"));
            browser.Navigate(CurrentUri);
        }

        public static bool FetchToken()
        {
            string uriString = CurrentUri.ToString();
            bool hasToken = false;

            if (uriString.Contains("access_token"))
            {
                int tokenStart = uriString.IndexOf('#') + 1;
                int expiresInStart = uriString.LastIndexOf('=') + 1;
                hasToken = true;

                string token = uriString.Substring(tokenStart, uriString.IndexOf('&') - tokenStart);

                token = token.Substring(token.IndexOf('=') + 1);

                string expiresIn = uriString.Substring(expiresInStart, uriString.Length - expiresInStart);

                // TODO token og expiresIn skal lagres i XML'en
                Debug.WriteLine(token);
                Debug.WriteLine(expiresIn);
            }

            return hasToken;
        }
    }
}
