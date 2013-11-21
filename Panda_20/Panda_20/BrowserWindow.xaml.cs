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
using MahApps.Metro.Controls;
using Panda_20.gui;
using Application = System.Windows.Application;

namespace Panda_20
{
    /// <summary>
    /// Logik til Web Browseren, der bruges til FB login.
    /// 
    /// Author: Frederik Olsen
    /// </summary>
    public partial class BrowserWindow : MetroWindow
    {

        private PageList _pageList;
 

        public BrowserWindow()
        {
            InitializeComponent();
            BrowserHelper.InitBrowser(Browser);
        }

        private void Browser_Navigating(object sender, NavigatingCancelEventArgs e)
        {

            if (!e.Uri.ToString().StartsWith("res://ieframe.dll"))
            {
                if (e.Uri != BrowserHelper.CurrentUri)
                {
                    BrowserHelper.CurrentUri = e.Uri;

                    if (BrowserHelper.FetchToken())
                    {
                        _pageList = new PageList();
                        Close();
                    }
                }
            }

            // Et URL, som begynder med "res://ieframe.dll" indikerer, at der er sket en
            // forbindelsesfejl, hvorved vi terminerer programmet.
            
            else
            {
                TerminationAssistant.ShowErrorPopUp("Panda was unable connect to the Internet! Click OK to close the program.");
            }

            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Service.ReadFromConfig("fb_token") == "")
            TerminationAssistant.ShowClosingPopUp(this, e);
        }
    }
}
