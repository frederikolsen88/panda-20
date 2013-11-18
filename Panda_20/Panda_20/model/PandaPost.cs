using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panda_20.model
{
    class PandaPost
    {
        private string actor_id;
        private string created_time;
        private string message;
        private string type;

        public PandaPost(string actorId, string createdTime, string message, string type)
        {
            actor_id = actorId;
            created_time = createdTime;
            this.message = message;
            this.type = type;
        }

        public string ActorId
        {
            get { return actor_id; }
            set { actor_id = value; }
        }

        public string CreatedTime
        {
            get { return created_time; }
            set { created_time = value; }
        }

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}
