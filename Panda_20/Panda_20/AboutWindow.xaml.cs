using System;
using System.Collections.Generic;
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
using MahApps.Metro.Controls;
using Panda_20.Properties;

namespace Panda_20
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : MetroWindow
    {
        public AboutWindow()
        {
            InitializeComponent();
            Version.Content = "Panda, version " + Settings.Default.version;
            Topmost = true;
            ShowInTaskbar = false;
        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
