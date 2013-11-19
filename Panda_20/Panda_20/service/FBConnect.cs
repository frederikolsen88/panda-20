using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facebook;
using Panda_20.model;

namespace Panda_20.service
{
    static class FBConnect
    {

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

            if (Service.LastSuccessfullFacebookUpdate == 0)
            {
                Service.LastSuccessfullFacebookUpdate = unix_time;
            }

            bool successfullconnect = true;
            // To test this, add "-60" to the below timestamp so that you have time to add a message somewhere. This is not needed once we get this method to run once a minute.
            long timestamp = Service.LastSuccessfullFacebookUpdate-500;
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
                                "SELECT fromid, text, time, post_id FROM comment WHERE post_id in (SELECT post_id FROM stream WHERE source_id='" +
                                Service.SelectedPage["id"] + "') AND time > " + timestamp,
                            comments_authors = "SELECT uid, name, friend_count, subscriber_count, pic_square FROM user WHERE uid IN (SELECT fromid FROM #comments)",
                            posts =
                                "SELECT actor_id, created_time, message, post_id FROM stream WHERE source_id = '" +
                                Service.SelectedPage["id"] + "' AND created_time > " + timestamp,
                            posts_authors = "SELECT uid, name, friend_count, subscriber_count, pic_square FROM user WHERE uid IN (SELECT actor_id FROM #posts)",
                            private_messages = "SELECT author_id, body, created_time FROM message WHERE thread_id IN (SELECT thread_id FROM thread WHERE folder_id = '0') AND created_time > " + timestamp,
                            private_messages_authors = "SELECT uid, name, friend_count, subscriber_count, pic_square FROM user WHERE uid IN (SELECT author_id FROM #private_messages)"
                        }
                    }) as JsonObject;
            }
            catch (FacebookOAuthException)
            {
                // TODO Håndter fejl
                successfullconnect = false;

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
            // COMMIT FOR SCIENCE
            ArrayList newNotifications = new ArrayList();
            ArrayList newUsers = new ArrayList();

            foreach (JsonObject data in (JsonArray)fqlresult["data"])
            {
                if (data["name"].Equals("comments"))
                {
                    foreach(JsonObject comment in (JsonArray)data["fql_result_set"])
                    {
                        string fromid = Convert.ToString(comment["fromid"]);
                        string time = Convert.ToString(comment["time"]);
                        string text = Convert.ToString(comment["text"]);
                        string post_id = Convert.ToString(comment["post_id"]);
                        PandaNotification pn = new PandaComment(fromid, time, text, post_id);
                        newNotifications.Add(pn);
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
                        newNotifications.Add(pn);
                    }
                }
                else if (data["name"].Equals("private_messages"))
                {
                    foreach (JsonObject private_message in (JsonArray) data["fql_result_set"])
                    {
                        string author_id = Convert.ToString(private_message["author_id"]);
                        string created_time = Convert.ToString(private_message["created_time"]);
                        string body = Convert.ToString(private_message["body"]);
                        PandaNotification pn = new PandaPrivateMessage(author_id, created_time, body);
                        newNotifications.Add(pn);
                    }
                }
                else
                {
                    foreach (JsonObject author in (JsonArray) data["fql_result_set"])
                    {
                        // uid, name, friend_count, subscriber_count, pic_square
                        string uid = Convert.ToString(author["uid"]);
                        string name = Convert.ToString(author["name"]);
                        string friend_count = Convert.ToString(author["friend_count"]);
                        string subscriber_count = Convert.ToString(author["subscriber_count"]);
                        string pic_square = Convert.ToString(author["pic_square"]);
                        PandaUser pu = new PandaUser(uid,name,friend_count,subscriber_count,pic_square);
                        newUsers.Add(pu);
                    }
                }
            }

            // Join dem
            for (int i = 0; i < newNotifications.Count; i++)
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
            }

            foreach (PandaNotification pn in newNotifications)
            {
                Service.CreateNotification(pn);
            }
        }

    }
}
