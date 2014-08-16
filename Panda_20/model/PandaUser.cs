using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Panda_20.model
{
    public class PandaUser
    {
        private string uid;
        private string name;
        private string friend_count;
        private string subscriber_count;
        private string pic_square;

        public PandaUser(string uid, string name, string friendCount, string subscriberCount, string picSquare)
        {
            this.uid = uid;
            this.name = name;
            friend_count = friendCount;
            subscriber_count = subscriberCount;
            pic_square = picSquare;
        }

        public string Uid
        {
            get { return uid; }
            set { uid = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string FriendCount
        {
            get { return friend_count; }
            set { friend_count = value; }
        }

        public string SubscriberCount
        {
            get { return subscriber_count; }
            set { subscriber_count = value; }
        }

        public string PicSquare
        {
            get { return pic_square; }
            set { pic_square = value; }
        }
    }
}
