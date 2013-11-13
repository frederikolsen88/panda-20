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
    class BrowserHelper
    {

        public static Uri CurrentUri { get; set; }

        public static void InitBrowser(WebBrowser browser)
        {
            // TODO URL skal komme fra AppValues

            CurrentUri = new Uri("https://www.facebook.com/dialog/oauth?client_id=244316138954589&redirect_uri=https://www.facebook.com/connect/login_success.html&scope=read_stream,manage_pages,read_page_mailboxes,offline_access&response_type=token");
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

                Debug.WriteLine(token);
                Debug.WriteLine(expiresIn);
            }

            return hasToken;
        }

        public static void Close(WebBrowser browser)
        {
            browser = null;
        }

        
    }
}
