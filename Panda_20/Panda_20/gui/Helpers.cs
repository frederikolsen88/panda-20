using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Facebook;

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
            CurrentUri = new Uri(Service.GetXmlElement("fbUrl"));
            browser.Navigate(CurrentUri);
        }

        public static bool FetchToken()
        {
            string uriString = CurrentUri.ToString();
            Debug.WriteLine(uriString);
            bool hasToken = false;

            if (uriString.Contains("access_token"))
            {
                int tokenStart = uriString.IndexOf('#') + 1;
                int expiresInStart = uriString.LastIndexOf('=') + 1;
                hasToken = true;

                string token = uriString.Substring(tokenStart, uriString.IndexOf('&') - tokenStart);

                token = token.Substring(token.IndexOf('=') + 1);

                string expiresIn = uriString.Substring(expiresInStart, uriString.Length - expiresInStart);

                Service.TokenAndExpiresIn[0] = token;
                Service.TokenAndExpiresIn[1] = expiresIn;
            }

            return hasToken;
        }
    }
}
