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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Panda_20.gui;

namespace Panda_20
{
    /// <summary>
    /// Interaction logic for BrowserWindow.xaml
    /// </summary>
    public partial class BrowserWindow : Window
    {
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
                    this.Close();
                    PageList pageList = new PageList();
                    pageList.Show();
                }
            }
        }
    }
}
