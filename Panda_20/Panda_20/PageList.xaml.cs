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
            PagesListBox.ItemsSource = Service.Instance.GetPages().Keys;

            // Hvis brugeren ikke administrerer nogen pages, viser vi en
            // popup om dette.
            if (!PagesListBox.HasItems)
            {
                const string message = "Panda kræver, at du administrerer mindst én Facebook-side. Klik OK for at lukke programmet.";
                const string caption = "Fejl";
                const MessageBoxButton button = MessageBoxButton.OK;
                const MessageBoxImage image = MessageBoxImage.Warning;

                MessageBoxResult result = MessageBox.Show(message, caption, button, image);

                if (result == MessageBoxResult.OK)
                {
                    // Når brugeren lukker DialogBoxen, må vi godt lukke programmet.
                    Application.Current.Shutdown();
                }
            }

        }

        private void PagesListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Service.Instance.SelectedPage = Service.Instance.GetPages()[PagesListBox.SelectedItem.ToString()];
            Service.Instance.SetPageFacebookClient((string)Service.Instance.SelectedPage["access_token"]);
            Hide();

            // TODO ... og så sker der ellers ting og sager.
        }
    }
}
