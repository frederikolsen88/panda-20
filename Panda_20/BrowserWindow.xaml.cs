﻿using System;
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

            if (BrowserHelper.IsConnectedToTheInternet())
            {
                MainWindow.NotifyIcon.Text = "Panda";

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

            // Manglende forbindelse til Google indikerer, at der er sket en
            // forbindelsesfejl, hvorved vi terminerer programmet.
            // Eller at Google er nede. Fat chance.
            
            else
            {
                // TerminationAssistant.ShowErrorPopUp(this, "Panda was unable connect to the Internet! Click OK to close the application.");
                MainWindow.NotifyIcon.ShowBalloonTip(5000, "Panda status", "Panda was unable to connect to the internet. Retrying...", ToolTipIcon.Info);
                MainWindow.NotifyIcon.Text = "Panda was unable to connect to the internet. Retrying...";

                Timer timer = new Timer();

                timer.Tick += new EventHandler(timer_Tick);
                timer.Interval = (10000)*(1);
                timer.Enabled = true;
                timer.Start();

            }          
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            BrowserHelper.InitBrowser(Browser);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Service.ReadFromConfig("fb_token") == "")
            TerminationAssistant.ShowClosingPopUp(this, e);
        }
    }
}
