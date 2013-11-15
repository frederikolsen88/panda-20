using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Panda_20.gui;
using Application = System.Windows.Application;

namespace Panda_20
{
    /// <summary>
    /// Logik til Web Browseren, der bruges til FB login.
    /// 
    /// Author: Frederik Olsen
    /// </summary>
    public partial class BrowserWindow : Window
    {

        private PageList _pageList;
 

        public BrowserWindow()
        {
            InitializeComponent();
            BrowserHelper.InitBrowser(Browser);
        }

        private void Browser_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if (e.Uri != BrowserHelper.CurrentUri)
            {
                BrowserHelper.CurrentUri = e.Uri;

                if (BrowserHelper.FetchToken())
                {
                    _pageList = new PageList();
                    _pageList.Show();
                    Close();
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            // Hvis pageListen ikke er spawned når browseren lukkes,
            // er det brugeren, der har gjort det. Så må vi godt
            // lukke alt ned. Det er ikke et kønt tjek, men det
            // virker lige nu.
            if (_pageList == null)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
