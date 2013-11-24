using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : MetroWindow
    {

        public OptionsWindow()
        {
            InitializeComponent();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.Save(); //  TODO This saves any changes made to the app-wide settings; if anything else is being done, that has to be handled as well.
            CloseOptionsWinodw(null, null);

        }

        private void CloseOptionsWinodw(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
