using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Panda_20.service
{
    internal static class SettingAccess
    {
        private static Boolean _own_omment_notifications;

        public static Boolean OwnCommentNotifications
        {
            get { return _own_omment_notifications; }
            set
            {
                //TODO 
            }
        }



        private static void WriteBooleanSetting(String name)
        {
            //TODO
        }

        private static void WriteIntegerSetting(int integerToWrite)
        {
            //TODO

        }
    }
}
