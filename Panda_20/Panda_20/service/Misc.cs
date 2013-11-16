using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panda_20.service
{
    class Misc
    {
        public static long UnixTimeNow()
        {
            TimeSpan _TimeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)_TimeSpan.TotalSeconds;
        }
    }
}
