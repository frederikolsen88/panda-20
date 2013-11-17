using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;
using Facebook;
using Panda_20.gui;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

namespace Panda_20
{
    /// <summary>
    /// Interaction logic for PageList.xaml
    /// </summary>
    public partial class PageList : Window
    {
        public PageList()
        {        
            InitializeComponent();
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
                const string message = "Panda kræver, at du administrerer mindst én Facebook-side. Klik OK for at lukke programmet.";
                const string caption = "Ingen sider fundet";
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
                string key = (string)PagesListBox.Items[0];
                Service.SelectedPage = Service.Pages[key];
                Service.SetPageFacebookClient((string)Service.SelectedPage["access_token"]);
                Close();
            }
            else
            {
                Show();
            }
        }

        private void LoadPictures()
        {
            foreach (KeyValuePair<string, string> pair in Service.PagePictures)
            {
                DisplayPage page = new DisplayPage(pair.Key, Service.GetImageFromUrl(pair.Value));
                PagesListBox.Items.Add(page);
            }
        }

        private void PagesListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Service.SelectedPage = Service.Pages[((DisplayPage) PagesListBox.SelectedItem).Name];
            Service.SetPageFacebookClient((string)Service.SelectedPage["access_token"]);
            Close();

            // TODO ... og så sker der ellers ting og sager.
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            // Hvis brugeren lukker pagelisten uden at have valgt
            // en side, burde vi kunne lukke programmet.
            if (Service.SelectedPage == null)
            {
                Application.Current.Shutdown();
            }
        }
        

        // Repræsentation af en Facebook-side som udelukkende skal
        // bruges til at få vist billede og navn i ListBox'en.
        public class DisplayPage
        {
            public DisplayPage(string name, Image image)
            {
                Name = name;
                Image = image;
            }

            public string Name { get; set; }

            public Image Image { get; set; }
        }
    }
}
