using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panda_20.model
{
    public class PandaNotification
    {
        protected string uid;
        protected string message;
        protected string created_time;
        protected string nid;

        protected PandaUser owner;

        public string Uid
        {
            get { return uid; }
            set { uid = value; }
        }

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        public string CreatedTime
        {
            get { return created_time; }
            set { created_time = value; }
        }

        public PandaUser Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        public string Nid
        {
            get { return nid; }
            set { nid = value; }
        }
    }
}
