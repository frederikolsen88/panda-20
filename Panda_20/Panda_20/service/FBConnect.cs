using System;
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
                                "SELECT actor_id, created_time, message, type FROM stream WHERE source_id = '" +
                                Service.SelectedPage["id"] + "' AND created_time > " + timestamp,
                            posts_authors = "SELECT uid, name, friend_count, subscriber_count, pic_square FROM user WHERE uid IN (SELECT actor_id FROM #posts)",
                            private_messages =
                                "SELECT sender, recipients, body, timestamp FROM unified_message WHERE thread_id IN (SELECT thread_id FROM unified_thread WHERE folder = 'inbox') AND (timestamp/1000) > " +
                                timestamp
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
            foreach (JsonObject data in (JsonArray)fqlresult["data"])
            {
                if (data["name"].Equals("comments"))
                {
                    foreach(JsonObject comment in (JsonArray)data["fql_result_set"])
                    {
                        string fromid = (string) comment["fromid"];
                        string time = (string) comment["time"];
                        string text = (string) comment["text"];
                        string post_id = (string) comment["post_id"];
                        PandaComment pc = new PandaComment(fromid, time, text, post_id);
                    }
                }
                else if (data["name"].Equals("posts"))
                {
                    foreach (JsonObject post in (JsonArray) data["fql_result_set"])
                    {
                        
                    }
                }
                else if (data["name"].Equals("private_messages"))
                {
                    
                }
            }
        }

    }
}
