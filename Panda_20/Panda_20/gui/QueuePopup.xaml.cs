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
using MahApps.Metro.Controls;
using Panda_20.model;
using Panda_20.service;
using Button = System.Windows.Controls.Button;

namespace Panda_20.gui
{
    /// <summary>
    /// Interaction logic for QueuePopup.xaml
    /// </summary>
    public partial class QueuePopup : Window
    {

        public QueuePopup(string queuecount, string visiblecount)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            ResizeMode = ResizeMode.NoResize;
            this.Left = SystemParameters.PrimaryScreenWidth - this.Width - 10;
            this.Top = GetTopOffset();
            this.Topmost = true;
            SetMessage("You have " + queuecount + " popups waiting to be shown, but there is no more room to show them!");
            SetName("Panda has run out of bamboo".ToUpper());
            SetImageUrl();
            SetType();
        }

        private int GetTaskbarHeight()
        {
            return Screen.PrimaryScreen.Bounds.Height - Screen.PrimaryScreen.WorkingArea.Height;
        }

        private double GetTopOffset()
        {
            return SystemParameters.PrimaryScreenHeight - GetTaskbarHeight()+5 - ((5 + this.Height) * (service.Queue.DisplayedNotifications.Count+1)) ; //1 skal være størrelsen af notification arrayet.
        }


        private void DismissButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            //Queue.AdjustPopups();
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

        private void SetImageUrl()
        {
            UserImage.Source = new BitmapImage(new Uri("../resources/exclamation.png", UriKind.Relative));
        }

        private void SetType()
        {
                IconImage.Source = new BitmapImage(new Uri("../resources/appbar.transit.construction.png", UriKind.Relative));
        }

        private void DismissButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ((Button) sender).Background = new SolidColorBrush(Colors.DarkTurquoise);

        }

        private void DismissButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var convertFromString = ColorConverter.ConvertFromString("#41B1E1");
            if (convertFromString != null)
                ((Button) sender).Background = new SolidColorBrush((Color) convertFromString);
        }

        public void RepositionMe()
        {
            // Doesn't fucking work
            //this.Top = GetTopOffset();
        }

        private void ClearVisible_Click(object sender, RoutedEventArgs e)
        {
            Queue.RemoveDisplayedPopups();
        }

        public void updateQueueCount(string queuecount)
        {
            this.SetMessage("You have " + queuecount + " popups waiting to be shown, but there is no more room to show them!");
        }

        private void ClearAll_Click(object sender, RoutedEventArgs e)
        {
            Queue.RemoveDisplayedAndQueuePopups();
        }
    }
}
