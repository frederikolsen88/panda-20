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

        public static void AddQueueNotification(PandaNotification np)
        {
            QueueNotifications.Add(np);
        }

        public static void RemoveQueueNotification(PandaNotification np)
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
                Queue.Qp.Hide();
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
            Qp.Hide();
            Service.QueueShown = false;
            InsertPopupsFromQueue();
        }

        public static void InsertPopupsFromQueue()
        {
            if (DisplayedNotifications.Count < Convert.ToInt32(Service.ReadFromConfig("notifications_max_amount")) && QueueNotifications.Count > 0)
            {
                // Get amount to insert into DisplayedNotifications
                int free = Math.Abs(DisplayedNotifications.Count - Convert.ToInt32(Service.ReadFromConfig("notifications_max_amount"))); // WHAT? det er jo bare   4 - displayed.Count???
                int countToInsert = Math.Min(QueueNotifications.Count, free);
                for (int i = 0; i < countToInsert; i++)
                {
                    
                        Console.WriteLine("Inserted POPUPS: " + i);
                        PandaNotification pn = (PandaNotification) QueueNotifications[0];
                        Service.CreateNotification(pn);
                        QueueNotifications.Remove(pn);
                        AdjustPopups();
                    
                }

                if (QueueNotifications.Count > 0)
                {
                    //Queue.Qp = new QueuePopup(Convert.ToString(Queue.QueueNotifications.Count),
                    //Convert.ToString(Queue.DisplayedNotifications.Count));
                    Queue.Qp.updateQueueCount(Convert.ToString(Queue.QueueNotifications.Count));
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
            Queue.Qp.Hide();
            Service.QueueShown = false;
        }

        public static void CheckColoursAndVisibility()
        {
            for (int i = DisplayedNotifications.Count - 1; i >= 0; i--)
            {
                NotificationPopup np = (NotificationPopup) DisplayedNotifications[i];
                if (Service.ReadFromConfig("colour_warnings") == "True")
                {
                    Int64 timeInFutureHalf = Convert.ToInt64(Service.ReadFromConfig("posts_time_limit"))*30 +
                                             Convert.ToInt64(np.Pn.CreatedTime);
                    Int64 timeInFuture = Convert.ToInt64(Service.ReadFromConfig("posts_time_limit"))*60 +
                                         Convert.ToInt64(np.Pn.CreatedTime);
                    if (Misc.UnixTimeNow(0) > timeInFutureHalf && Misc.UnixTimeNow(0) < timeInFuture)
                    {
                        np.changetopColor(255, 128, 0);
                    }
                    else if (Misc.UnixTimeNow(0) > timeInFuture)
                    {
                        np.changetopColor(255, 11, 15);
                    }
                }

                if (np.Pn.GetType().ToString() == "Panda_20.model.PandaComment")
                {
                    if (Service.ReadFromConfig("comments_time_popdown_enabled") == "True")
                    {
                        Int64 shownUnixTS = np.ShownUnixTs;
                        Int64 currentTS = Misc.UnixTimeNow(0);
                        int popdowntime = Convert.ToInt32(Service.ReadFromConfig("comments_time_popdown")) - 2;

                        if ((currentTS - shownUnixTS) > popdowntime)
                        {
                            np.DismissButton_OnClick(np, null);
                        }
                    }
                }
                else if (np.Pn.GetType().ToString() == "Panda_20.model.PandaPost")
                {
                    if (Service.ReadFromConfig("posts_time_popdown_enabled") == "True")
                    {
                        Int64 shownUnixTS = np.ShownUnixTs;
                        Int64 currentTS = Misc.UnixTimeNow(0);
                        int popdowntime = Convert.ToInt32(Service.ReadFromConfig("posts_time_popdown")) - 2;

                        if ((currentTS - shownUnixTS) > popdowntime)
                        {
                            np.DismissButton_OnClick(np, null);
                        }
                    }
                }
                else if (np.Pn.GetType().ToString() == "Panda_20.model.PandaPrivateMessage")
                {
                    if (Service.ReadFromConfig("pm_time_popdown_enabled") == "True")
                    {
                        Int64 shownUnixTS = np.ShownUnixTs;
                        Int64 currentTS = Misc.UnixTimeNow(0);
                        int popdowntime = Convert.ToInt32(Service.ReadFromConfig("pm_time_popdown")) - 2;

                        if ((currentTS - shownUnixTS) > popdowntime)
                        {
                            np.DismissButton_OnClick(np, null);
                        }
                    }
                }
            }
        }

    }
}
