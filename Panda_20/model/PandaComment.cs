using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Panda_20.model
{
    public class PandaComment:PandaNotification
    {
        private string post_id;

        public PandaComment(string uid, string createdTime, string message, string nid, string post_id)
        {
            this.Uid = uid;
            this.CreatedTime = createdTime;
            this.Message = message;
            this.Nid = nid;
            this.PostId = post_id;
        }

        public string PostId
        {
            get { return post_id; }
            set { post_id = value; }
        }
    }
}
