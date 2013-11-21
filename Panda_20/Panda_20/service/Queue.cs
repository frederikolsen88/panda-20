using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Panda_20.gui;
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

        public static void AddNotification(NotificationPopup np)
        {
            DisplayedNotifications.Add(np);
        }

        public static void RemoveNotification(NotificationPopup np)
        {
            DisplayedNotifications.Remove(np);
        }

        public static void AdjustPopups()
        {
            
        }

    }
}
