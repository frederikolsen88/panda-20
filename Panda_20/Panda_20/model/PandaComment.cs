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

        public PandaComment(string uid, string createdTime, string message, string nid)
        {
            this.Uid = uid;
            this.CreatedTime = createdTime;
            this.Message = message;
            this.Nid = nid;
        }
    }
}
