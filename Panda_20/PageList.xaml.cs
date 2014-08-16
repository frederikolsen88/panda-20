using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
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
using System.Windows.Shapes;
using Facebook;
using MahApps.Metro.Controls;
using Panda_20.gui;
using Panda_20.service;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

namespace Panda_20
{
    /// <summary>
    /// Interaction logic for PageList.xaml
    /// </summary>
    public partial class PageList : MetroWindow {
    
        public PageList()
        {        
            InitializeComponent();
            ShowInTaskbar = false;
            InitPageList();
        }

        private void InitPageList()
        {
            Hide();
            Service.FetchPages();
            LoadPictures();

            // TESTS
            // string firstKey = Service.Instance.Pages.Keys.First();
            // JsonObject firstValue = Service.Instance.Pages.Values.First();
            // Service.Instance.Pages.Clear();
            // Service.Instance.Pages.Add(firstKey, firstValue);

            // Hvis brugeren ikke administrerer nogen pages, viser vi en
            // popup om dette.

            if (PagesListBox.Items.Count == 0)
            {
                const string message = "Panda requires that you are an administrator of at least one Facebook page. Click OK to close the application.";
                const string caption = "No pages found";
                const MessageBoxButton button = MessageBoxButton.OK;
                const MessageBoxImage image = MessageBoxImage.Warning;

                MessageBoxResult result = MessageBox.Show(message, caption, button, image);

                if (result == MessageBoxResult.OK)
                {
                    // Når brugeren lukker MessageBoxen, må vi godt lukke programmet.
                    Application.Current.Shutdown();
                }
            }

            // Brugeren har kun én Facebook-side, som vælges by default.
            else if (PagesListBox.Items.Count == 1)
            {
                string key = ((DisplayPage)PagesListBox.Items[0]).Name;
                Service.SelectedPage = Service.Pages[key];
                Service.SetPageFacebookClient((string)Service.SelectedPage["access_token"]);
                connectedToPageToolTip();
                Close();
            }
            else
            {
                Show();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Service.SelectedPage == null)
            {
                TerminationAssistant.ShowClosingPopUp(this, e);         
            }
                
        }

        private void LoadPictures()
        {
            foreach (KeyValuePair<string, string> pair in Service.PagePictures)
            {
                DisplayPage page = new DisplayPage(pair.Key, Misc.DownloadImage(pair.Value));

                string id = (string) Service.Pages[pair.Key]["id"];
                page.Likes = Service.GetLikes(id);
                PagesListBox.Items.Add(page);

            }
        }

        private void PagesListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Misc.CheckConnection("http://www.facebook.com"))
            {
                Service.SelectedPage = Service.Pages[((DisplayPage) PagesListBox.SelectedItem).Name];
                Service.SetPageFacebookClient((string) Service.SelectedPage["access_token"]);
                connectedToPageToolTip();
                Close();
            }

            else
            {
                TerminationAssistant.ShowErrorPopUp(this, "Panda was unable to connect you to the chosen page. Click OK to close the application.");
            }          
        }

        private void connectedToPageToolTip()
        {
            MainWindow.NotifyIcon.ShowBalloonTip(5000, "Panda status", "Panda is connected to " + Service.SelectedPage["name"] + ".", ToolTipIcon.Info);
            MainWindow.NotifyIcon.Text = "Panda is connected to " + Service.SelectedPage["name"] + ".";
        }

        // Repræsentation af en Facebook-side som udelukkende skal
        // bruges til at få vist billede og navn i ListBox'en.
        public class DisplayPage
        {
            public DisplayPage(string name, BitmapImage image)
            {
                Name = name;
                Image = image;
            }

            public string Name { get; set; }

            public BitmapImage Image { get; set; }

            public Int32 Likes { get; set; }
        }
    }
}
