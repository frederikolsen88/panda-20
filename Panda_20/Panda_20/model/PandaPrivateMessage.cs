using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panda_20.model
{
    class PandaPrivateMessage
    {
        private string author_id;
        private string body;
        private string created_time;

        public PandaPrivateMessage(string authorId, string body, string createdTime)
        {
            author_id = authorId;
            this.body = body;
            created_time = createdTime;
        }

        public string AuthorId
        {
            get { return author_id; }
            set { author_id = value; }
        }

        public string Body
        {
            get { return body; }
            set { body = value; }
        }

        public string CreatedTime
        {
            get { return created_time; }
            set { created_time = value; }
        }
    }
}
