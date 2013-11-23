﻿using System;
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
        private static QueuePopup qp = new QueuePopup("0", "0");

        public static ArrayList DisplayedNotifications
        {
            get { return displayedNotifications; }
        }

        public static ArrayList QueueNotifications
        {
            get { return queueNotifications; }
        }

        public static QueuePopup Qp
        {
            get { return qp; }
            set { qp = value; }
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
            if (QueueNotifications.Count == 0)
            {
                Queue.Qp.Close();
                Service.QueueShown = false;
            }
            else
            {
                Queue.Qp.updateQueueCount(Convert.ToString(Queue.QueueNotifications.Count));
            }
            
        }

        public static void RemoveDisplayedPopups()
        {
            foreach (NotificationPopup displayedNotification in DisplayedNotifications)
            {
                displayedNotification.Close();
            }
            DisplayedNotifications.Clear();
            Qp.Close();
            Service.QueueShown = false;
            InsertPopupsFromQueue();
        }

        public static void InsertPopupsFromQueue()
        {
            if (DisplayedNotifications.Count < 4 && QueueNotifications.Count > 0)
            {
                // Get amount to insert into DisplayedNotifications
                int countToInsert = Math.Abs(DisplayedNotifications.Count - 4); // WHAT? det er jo bare   4 - displayed.Count???
                int currentQueueCount = QueueNotifications.Count;
                Console.WriteLine("currentQueueCount: " + currentQueueCount);
                for (int i = 0; i < countToInsert; i++)
                {
                    while (QueueNotifications.Count > i)
                    {
                        Console.WriteLine("Inserted POPUPS: " + i);
                        NotificationPopup np = (NotificationPopup) QueueNotifications[i];
                        AddDisplayedNotification(np);
                        QueueNotifications.Remove(np);
                        AdjustPopups();
                        np.Show();
                    }
                }

                if (QueueNotifications.Count > 0)
                {
                    Queue.Qp = new QueuePopup(Convert.ToString(Queue.QueueNotifications.Count),
                    Convert.ToString(Queue.DisplayedNotifications.Count));
                    Queue.Qp.Show();
                    Service.QueueShown = true;
                }
            }
        }

        public static void RemoveDisplayedAndQueuePopups()
        {
            foreach (NotificationPopup displayedNotification in DisplayedNotifications)
            {
                displayedNotification.Close();
            }
            DisplayedNotifications.Clear();
            QueueNotifications.Clear();
            Queue.Qp.Close();
            Service.QueueShown = false;
        }

    }
}
