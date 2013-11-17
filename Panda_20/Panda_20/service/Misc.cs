using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panda_20.service
{
    class Misc
    {
        // Accepts seconds to add or subtract
        public static long UnixTimeNow(long seconds)
        {
            TimeSpan _TimeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)_TimeSpan.TotalSeconds+seconds;
        }
    }
}