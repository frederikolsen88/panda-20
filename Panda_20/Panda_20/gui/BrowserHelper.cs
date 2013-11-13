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
            // TODO URL fra AppValues
            browser.Navigate("http://www.google.com");
        }

        
    }
}
