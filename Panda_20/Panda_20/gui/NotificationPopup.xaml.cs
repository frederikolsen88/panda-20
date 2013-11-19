using System;
using System.Collections.Generic;
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
using Panda_20.service;

namespace Panda_20.gui
{
    /// <summary>
    /// Interaction logic for NotificationPopup.xaml
    /// </summary>
    public partial class NotificationPopup : Window
    {
        public NotificationPopup(string message, string name, string imageUrl, string type)
        {
            InitializeComponent();
            this.Left = SystemParameters.PrimaryScreenWidth - this.Width - 10;
            this.Top = GetTopOffset();
            this.Topmost = true;
            SetMessage(message);
            SetName(name);
            SetImageUrl(imageUrl);
            SetType(type);
            //DismissImage = billede af et kryds i guess
        }

        private int GetTaskbarHeight()
        {
            return Screen.PrimaryScreen.Bounds.Height - Screen.PrimaryScreen.WorkingArea.Height;
        }

        private double GetTopOffset()
        {
            return SystemParameters.PrimaryScreenHeight - GetTaskbarHeight() - ((10 + this.Height) * 1) ; //1 skal være størrelsen af notification arrayet.
        }


        private void DismissButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void FacebookButton_OnClick(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.google.com"); //URL til fb posten
        }

        private void changetopColor()
        {
            // Der skal nok laves om i designet :S
        }

        private void SetMessage(string message)
        {
            Message.Text = message;
        }

        private void SetName(string name)
        {
            UserName.Text = name;
        }

        private void SetImageUrl(string imageUrl)
        {
            UserImage.Source = Misc.DownloadImage(imageUrl);
        }

        private void SetType(string type)
        {
            // TODO
        }
    }
}
