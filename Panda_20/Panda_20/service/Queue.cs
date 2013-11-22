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
        private static ArrayList queueNotifications = new ArrayList();

        public static ArrayList DisplayedNotifications
        {
            get { return displayedNotifications; }
        }

        public static ArrayList QueueNotifications
        {
            get { return queueNotifications; }
        }

        public static void AddQueueNotification(NotificationPopup np)
        {
            QueueNotifications.Add(np);
        }

        public static void RemoveQueueNotification(NotificationPopup np)
        {
            QueueNotifications.Remove(np);
        }

        public static void AddDisplayedNotification(NotificationPopup np)
        {
            DisplayedNotifications.Add(np);
        }

        public static void RemoveDisplayedNotification(NotificationPopup np)
        {
            DisplayedNotifications.Remove(np);
        }

        public static void AdjustPopups()
        {
            for (int i = 0; i < DisplayedNotifications.Count; i++)
            {
                NotificationPopup np = (NotificationPopup) DisplayedNotifications[i];
                np.RepositionMe(DisplayedNotifications.Count-i);
            }
        }

        public static void removeVisiblePopups()
        {
            foreach (NotificationPopup displayedNotification in DisplayedNotifications)
            {
                displayedNotification.Close();
            }
            DisplayedNotifications.Clear();
        }

    }
}
