using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
using Facebook;
using Panda_20.gui;
using Panda_20.model;

namespace Panda_20.service
{
    static class FBConnect
    {

        private static ArrayList oldNotifications = new ArrayList();
        private static PandaUser defaultPagePandaUser = new PandaUser(Convert.ToString(Service.SelectedPage["id"]), Convert.ToString(Service.SelectedPage["name"]), "0", "0", "http://graph.facebook.com/" + Convert.ToString(Service.SelectedPage["id"]) + "/picture");
        private static ArrayList commentsOwnPosts = new ArrayList();
        private static int count = 0;
        private static bool connected = true;

        public static void OneMinuteTimer()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(TimerDoStuff);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Start();
        }

        public static void TimerDoStuff(object sender, EventArgs e)
        {
            if (count == 2)
            {
                if (Misc.CheckConnection())
                {
                    if (!connected)
                        MainWindow.NotifyIcon.ShowBalloonTip(5000, "Panda status", "Panda has restored the connection.", ToolTipIcon.Info);
                    
                    GetFacebookUpdates();

                    connected = true;
                }

                else
                {
                    MainWindow.NotifyIcon.ShowBalloonTip(5000, "Panda status", "Panda was unable to connect to Facebook. Retrying...", ToolTipIcon.Info);
                    connected = false;
                }
                    
                count = 0;    
            }

            Queue.CheckColoursAndVisibility();

            count++;
        }

        //-----------------------------------------------------------
        //--------------<Super Duper 1 Minute Method>---------Author: HJTH 
        //-----------------------------------------------------------
        // Ask Facebook what's up! Method needs to run asynchronously to work.

        public static async void GetFacebookUpdates()
        {
            Console.WriteLine("UpdateFBMethod");

            // This is simply for monitoring how long this runs
            long unix_time = Misc.UnixTimeNow(0);
            Console.WriteLine("unix before: " + unix_time);

            bool successfullconnect = true;
            long timestamp = Service.LastSuccessfullFacebookUpdate-5;
            JsonObject result = new JsonObject();

            // Collecting the data. I still need to get comment_authors in the same way I did post_authors.
            try
            {
                result = await Service.PageClient.GetTaskAsync("fql",
                    new
                    {
                        q = new
                        {
                            comments =
                                "SELECT fromid, text, time, id, post_id FROM comment WHERE post_id in (SELECT post_id FROM stream WHERE source_id='" +
                                Service.SelectedPage["id"] + "' LIMIT 100) AND time > '" + timestamp + "' ORDER BY time ASC LIMIT 1000",
                            comments_authors = "SELECT uid, name, friend_count, subscriber_count, pic_square FROM user WHERE uid IN (SELECT fromid FROM #comments)",
                            comments_ownposts = "SELECT post_id FROM stream WHERE source_id = '" +
                            Service.SelectedPage["id"] + "' AND post_id IN (SELECT post_id FROM #comments) LIMIT 100",
                            posts =
                                "SELECT actor_id, created_time, message, post_id FROM stream WHERE source_id = '" +
                                Service.SelectedPage["id"] + "' AND created_time > '" + timestamp + "' ORDER BY created_time ASC LIMIT 100",
                            posts_authors = "SELECT uid, name, friend_count, subscriber_count, pic_square FROM user WHERE uid IN (SELECT actor_id FROM #posts)",
                            private_messages = "SELECT author_id, body, created_time, message_id FROM message WHERE thread_id IN (SELECT thread_id FROM thread WHERE folder_id = '0' LIMIT 100) AND created_time > '" + timestamp + "' ORDER BY created_time ASC LIMIT 100",
                            private_messages_authors = "SELECT uid, name, friend_count, subscriber_count, pic_square FROM user WHERE uid IN (SELECT author_id FROM #private_messages)"
                        }
                    }) as JsonObject;
            }
            catch (FacebookOAuthException)
            {
                successfullconnect = false;
                TerminationAssistant.ShowErrorPopUp(null, "Panda did not receive the neccessary permissions from Facebook. Click OK to close the program.");

            }

            // Here we save the current unix time BEFORE working with the data. This might be enough to ensure that we will never miss anything? Probably not though...
            long unix_timeAfter = Misc.UnixTimeNow(0);
            if (successfullconnect)
            {
                Service.LastSuccessfullFacebookUpdate = unix_timeAfter;
            }

            Console.WriteLine("FQL result: " + result.ToString());

            Console.WriteLine("unix: " + unix_timeAfter);

            createModelObjects(result);
        }

        public static void createModelObjects(JsonObject fqlresult)
        {
            
            ArrayList newNotifications = new ArrayList();
            ArrayList newUsers = new ArrayList();
            newUsers.Add(defaultPagePandaUser);

            foreach (JsonObject data in (JsonArray)fqlresult["data"])
            {
                if (data["name"].Equals("comments"))
                {
                    foreach(JsonObject comment in (JsonArray)data["fql_result_set"])
                    {
                        string fromid = Convert.ToString(comment["fromid"]);
                        string time = Convert.ToString(comment["time"]);
                        string text = Convert.ToString(comment["text"]);
                        string id = Convert.ToString(comment["id"]);
                        string post_id = Convert.ToString(comment["post_id"]);
                        PandaNotification pn = new PandaComment(fromid, time, text, id, post_id);
                        Console.WriteLine("TILFØJET: " + pn.Message);
                        if (Service.ReadFromConfig("comments_display_notifications") == "True")
                        {
                            newNotifications.Add(pn);
                        }
                    }
                }
                else if (data["name"].Equals("posts"))
                {
                    foreach (JsonObject post in (JsonArray)data["fql_result_set"])
                    {
                        string actor_id = Convert.ToString(post["actor_id"]);
                        string created_time = Convert.ToString(post["created_time"]);
                        string message = Convert.ToString(post["message"]);
                        string post_id = Convert.ToString(post["post_id"]);
                        PandaNotification pn = new PandaPost(actor_id, created_time, message, post_id);
                        if (Service.ReadFromConfig("posts_display_notifications") == "True")
                        {
                            newNotifications.Add(pn);
                        }
                    }
                }
                else if (data["name"].Equals("private_messages"))
                {
                    foreach (JsonObject private_message in (JsonArray) data["fql_result_set"])
                    {
                        string author_id = Convert.ToString(private_message["author_id"]);
                        string created_time = Convert.ToString(private_message["created_time"]);
                        string body = Convert.ToString(private_message["body"]);
                        string message_id = Convert.ToString(private_message["message_id"]);
                        PandaNotification pn = new PandaPrivateMessage(author_id, created_time, body, message_id);
                        if (Service.ReadFromConfig("pm_display_notifications") == "True")
                        {
                            newNotifications.Add(pn);
                        }
                    }
                }
                else if (data["name"].Equals("comments_ownposts"))
                {
                    foreach (JsonObject ownposts in (JsonArray)data["fql_result_set"])
                    {
                        string post_id = Convert.ToString(ownposts["post_id"]);
                        string[] pizza = post_id.Split('_');
                        commentsOwnPosts.Add(pizza);
                    }
                }
                else
                {
                    foreach (JsonObject author in (JsonArray) data["fql_result_set"])
                    {
                        // uid, name, friend_count, subscriber_count, pic_square
                        string uid = Convert.ToString(author["uid"]);
                        string name = Convert.ToString(author["name"]);
                        string friend_count =  author["friend_count"] != null ? Convert.ToString(author["friend_count"]) : "0";
                        string subscriber_count = author["subscriber_count"] != null ? Convert.ToString(author["subscriber_count"]) : "0";
                        string pic_square = Convert.ToString(author["pic_square"]);
                        PandaUser pu = new PandaUser(uid,name,friend_count,subscriber_count,pic_square);
                        newUsers.Add(pu);
                    }
                }
            }

            // Join dem
            for (int i = newNotifications.Count - 1; i >= 0; i--)
            {
                string uid = "";
                PandaNotification pn = (PandaNotification) newNotifications[i];
                uid = pn.Uid;

                foreach (PandaUser newUser in newUsers)
                {
                    if (newUser.Uid.Equals(uid))
                    {
                        pn.Owner = newUser;
                    }
                }

                if (pn.GetType().ToString() == "Panda_20.model.PandaComment" && Service.ReadFromConfig("comments_display_comments_on_own_post") == "False")
                {
                    foreach (string[] ownPost in commentsOwnPosts)
                    {
                        PandaComment pc = (PandaComment) pn;
                        if (pc.PostId == ownPost[1] && ownPost[0] == Service.SelectedPage["id"])
                        {
                            newNotifications.Remove(pc);
                        }
                    }
                }
            }

            Console.WriteLine(newNotifications.Count);
            Console.WriteLine(oldNotifications.Count);

            foreach (PandaNotification pn in newNotifications)
            {
                Console.WriteLine("POPUP: " + pn.Message);
                bool duplicate = false;
                foreach (PandaNotification oldNotification in oldNotifications)
                {
                    if (pn.Nid.Equals(oldNotification.Nid))
                    {
                        Console.WriteLine("PN: " + pn.Nid + "PN M:" + pn.Message);
                        Console.WriteLine("PN OLD: " + oldNotification.Nid + "PN OLD M: " + oldNotification.Message);
                        duplicate = true;
                    }
                }
                if (!duplicate)
                {

                    // Tjek for egne posts + kommentarer på egne posts
                    if (pn.GetType().ToString() == "Panda_20.model.PandaPost")
                    {
                        if (Service.ReadFromConfig("posts_display_own") == "False")
                        {
                            if (pn.Owner.Uid != (string) Service.SelectedPage["id"])
                            {
                                Service.CreateNotification(pn);
                            }
                        }
                        else
                        {
                            Service.CreateNotification(pn);
                        }
                    }
                    else if (pn.GetType().ToString() == "Panda_20.model.PandaComment")
                    {
                        if (Service.ReadFromConfig("comments_display_own") == "False")
                        {
                            if (pn.Owner.Uid != (string)Service.SelectedPage["id"])
                            {
                                Service.CreateNotification(pn);
                            }
                        }
                        else
                        {
                            Service.CreateNotification(pn);
                        }
                    }
                    else
                    {
                        Service.CreateNotification(pn);
                    }
                    
                    
                }
            }

            oldNotifications = newNotifications;
        }

    }
}
