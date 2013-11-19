using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Panda_20.model;

namespace Panda_20.service
{
    public static class Queue
    {
        private static ArrayList displayedNotifications = new ArrayList();

        public static ArrayList DisplayedNotifications
        {
            get { return displayedNotifications; }
        }

        public static void AddNotification(PandaNotification pn)
        {
            DisplayedNotifications.Add(pn);
        }

        public static void RemoveNotification(PandaNotification pn)
        {
            DisplayedNotifications.Remove(pn);
        }



    }
}
