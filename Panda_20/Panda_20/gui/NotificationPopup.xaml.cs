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

namespace Panda_20.gui
{
    /// <summary>
    /// Interaction logic for NotificationPopup.xaml
    /// </summary>
    public partial class NotificationPopup : MetroWindow
    {
        private string nid;
        private PandaNotification pn;
        public NotificationPopup(PandaNotification pn)
        {
            InitializeComponent();
            this.Left = SystemParameters.PrimaryScreenWidth - this.Width - 10;
            this.Top = GetTopOffset();
            this.Topmost = true;
            this.Pn = pn;
            SetMessage(pn.Message);
            SetName(pn.Owner.Name);
            SetImageUrl(pn.Owner.PicSquare);
            SetType(pn.GetType().ToString());
            SetUserFriends(Convert.ToString(Convert.ToInt32(pn.Owner.FriendCount) + Convert.ToInt32(pn.Owner.SubscriberCount)));
            this.Nid = pn.Nid;
            //DismissImage = billede af et kryds i guess
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
            PandaNotification thisNotification = null;
            foreach (PandaNotification pd in service.Queue.DisplayedNotifications)
            {
                if (pd.Nid == this.Nid)
                {
                    thisNotification = pd;
                }
            }
            service.Queue.RemoveNotification(thisNotification);
        }

        private void FacebookButton_OnClick(object sender, RoutedEventArgs e)
        {
            string[] pizza = Nid.Split('_');
            System.Diagnostics.Process.Start("https://www.facebook.com/permalink.php?story_fbid=" + pizza[1] + "&id=" + pizza[0]); //URL til fb posten
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

    }
}
