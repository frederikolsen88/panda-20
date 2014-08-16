using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panda_20.model
{
    public class PandaPrivateMessage:PandaNotification
    {

        public PandaPrivateMessage(string uid, string createdTime, string message, string nid)
        {
            this.uid = uid;
            this.Message = message;
            this.CreatedTime = createdTime;
            this.Nid = nid;
        }

    }
}
