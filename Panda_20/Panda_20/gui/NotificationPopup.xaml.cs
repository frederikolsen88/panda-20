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
    /// Interaction logic for NotificationPopup.xaml
    /// </summary>
    public partial class NotificationPopup : Window
    {
        private string nid;
        private PandaNotification pn;
        private long shownUnixTS;
        public NotificationPopup(PandaNotification pn)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            ResizeMode = ResizeMode.NoResize;
            this.Left = SystemParameters.PrimaryScreenWidth - this.Width - 10;
            this.Top = GetTopOffset(0);
            this.Topmost = true;
            this.Pn = pn;
            SetMessage(pn.Message);
            SetName(pn.Owner.Name.ToUpper());
            SetImageUrl(pn.Owner.PicSquare);
            SetType(pn.GetType().ToString());
            SetUserFriends(Convert.ToString(Convert.ToInt32(pn.Owner.FriendCount) + Convert.ToInt32(pn.Owner.SubscriberCount)));
            this.Nid = pn.Nid;
            this.shownUnixTS = Misc.UnixTimeNow(0);
        }

        public string Nid
        {
            get { return nid; }
            set { nid = value; }
        }

        public PandaNotification Pn
        {
            get { return pn; }
            set { pn = value; }
        }

        public long ShownUnixTs
        {
            get { return shownUnixTS; }
            set { shownUnixTS = value; }
        }

        private int GetTaskbarHeight()
        {
            return Screen.PrimaryScreen.Bounds.Height - Screen.PrimaryScreen.WorkingArea.Height;
        }

        private double GetTopOffset(int order)
        {
            return SystemParameters.PrimaryScreenHeight - GetTaskbarHeight()+5 - ((5 + this.Height) * ((service.Queue.DisplayedNotifications.Count+1)-order)) ; //1 skal være størrelsen af notification arrayet.
        }


        private void DismissButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            //PandaNotification thisNotification = null;
            //foreach (NotificationPopup np in service.Queue.DisplayedNotifications)
            //{
            //    if (np.Pn.Nid == this.Nid)
            //    {
            //        thisNotification = np.Pn;
            //    }
            //}
            service.Queue.RemoveDisplayedNotification(this);
            Queue.AdjustPopups();
            Queue.InsertPopupsFromQueue();
        }

        private void FacebookButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (pn.GetType().ToString().Equals("Panda_20.model.PandaComment"))
            {
                PandaComment pc = (PandaComment) Pn;
                string[] pizza = pc.PostId.Split('_');
                string[] burger = pc.Nid.Split('_');
                System.Diagnostics.Process.Start("https://www.facebook.com/permalink.php?story_fbid=" + pizza[1] +
                                                 "&id=" + pizza[0] + "&comment_id=" + burger[1]); //URL til fb posten
            }
            else if (pn.GetType().ToString().Equals("Panda_20.model.PandaPrivateMessage"))
            {
                string[] pizza = Nid.Split('_');
                System.Diagnostics.Process.Start(Service.SelectedPage["link"] + "?sk=messages_inbox&action=read&tid=id." + pizza[0]); //URL til fb posten
            }
            else
            {
                string[] pizza = Nid.Split('_');
                System.Diagnostics.Process.Start("https://www.facebook.com/permalink.php?story_fbid=" + pizza[1] + "&id=" + pizza[0]); //URL til fb posten
            }
            DismissButton_OnClick(this, null);
        }

        public void changetopColor(byte r, byte g, byte b)
        {
            ColorRectangle.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(r, g, b));
            DismissButton.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(r, g, b));
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
            if (type.Equals("Panda_20.model.PandaPrivateMessage"))
            {
                IconImage.Source = new BitmapImage(new Uri("../resources/pm.png", UriKind.Relative));
            }
            if (type.Equals("Panda_20.model.PandaPost"))
            {
                IconImage.Source = new BitmapImage(new Uri("../resources/post.png", UriKind.Relative));
            }
            if (type.Equals("Panda_20.model.PandaComment"))
            {
                IconImage.Source = new BitmapImage(new Uri("../resources/comment.png", UriKind.Relative));
            }
        }

        private void SetUserFriends(string userFriends)
        {
            UserFriends.Text = userFriends;
        }

        private void DismissButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
           // ((Button) sender).Background = new SolidColorBrush(Colors.GhostWhite);
            ((Button) sender).Opacity = 0.5;

        }

        private void DismissButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
          //  var convertFromString = ColorConverter.ConvertFromString("#FFF8F8FF");
          //  if (convertFromString != null)
          //      ((Button) sender).Background = new SolidColorBrush((Color) convertFromString);
            ((Button)sender).Opacity = 1;
        }

        public void RepositionMe(int order)
        {
            //Doesn't fucking work
            this.Top = GetTopOffset(order);
        }

    }
}
