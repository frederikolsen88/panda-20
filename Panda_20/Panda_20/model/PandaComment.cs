using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Panda_20.model
{
    class PandaComment
    {
        private string fromid;
        private string time;
        private string text;
        private string post_id;
        private PandaUser owner;
        // Hej

        public PandaComment(string fromid, string time, string text, string post_id)
        {
            this.fromid = fromid;
            this.time = time;
            this.text = text;
            this.post_id = post_id;
        }

        public string Fromid
        {
            get { return fromid; }
            set { fromid = value; }
        }

        public string Time
        {
            get { return time; }
            set { time = value; }
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public string PostId
        {
            get { return post_id; }
            set { post_id = value; }
        }

        public PandaUser Owner
        {
            get { return owner; }
            set { owner = value; }
        }
    }
}
