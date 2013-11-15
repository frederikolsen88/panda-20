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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
