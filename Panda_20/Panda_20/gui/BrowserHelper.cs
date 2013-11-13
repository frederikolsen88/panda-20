using System;
using System.Collections.Generic;
using System.Linq;
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
            browser.Navigate("https://www.facebook.com/dialog/oauth?client_id=244316138954589&redirect_uri=https://www.facebook.com/connect/login_success.html&scope=read_stream,manage_pages,read_page_mailboxes,offline_access&response_type=token");
        }

        
    }
}
