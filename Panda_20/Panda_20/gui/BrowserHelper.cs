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

        public static void FetchToken()
        {
            string uriString = CurrentUri.ToString();

            if (uriString.Contains("access_token"))
            {
                int start = uriString.IndexOf('=', 0);
                int end = uriString.IndexOf('&', start);
                string accessToken = uriString.Substring(start + 1, end - start);
                Debug.WriteLine(accessToken);
            }
        }

        
    }
}
