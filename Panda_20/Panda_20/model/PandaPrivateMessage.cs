using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panda_20.model
{
    public class PandaPrivateMessage:PandaNotification
    {

        public PandaPrivateMessage(string uid, string message, string createdTime)
        {
            this.uid = uid;
            this.Message = message;
            this.CreatedTime = createdTime;
        }

    }
}
