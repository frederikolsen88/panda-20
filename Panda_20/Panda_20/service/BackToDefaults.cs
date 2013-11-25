using System;
using System.Collections.Generic;
using System.Linq;
using Panda_20.Properties;

namespace Panda_20.service
{
    static class BackToDefaults
    {
        private static Settings sd = Settings.Default;

        public static void DefaultSettings()
        {
            //TODO metode der resetter værdierne i Settings.Default
            // Remember to UPDATE this method every once in a while.


            // COMMENTS
            sd.comments_display_comments_on_own_post = false;
            sd.comments_display_notifications = true;
            sd.comments_display_own = false;
            sd.comments_play_sound = true;
            sd.comments_time_limit = 5;

            // MESSAGES
            sd.pm_display_notifications = true;
            sd.pm_play_sound = true;
            sd.pm_time_limit = 5;
            
            // NOTIFICATIONS GENERALLY
            sd.notifications_max_amount = 4;
            sd.notifications_play_sound = true;

            // POSTS
            sd.posts_display_notifications = true;
            sd.posts_display_own = false;
            sd.posts_play_sound = true;
            sd.posts_time_limit = 5;

            // OTHERS
            sd.start_with_windows = false;
            sd.Save();


        }

        public static void DefaultFacebookValues()
        {
            sd.fb_token = "";
            sd.fb_token_expires_in = "";
            sd.Save();
        }

        public static void ResetEverything()
        {
            DefaultFacebookValues();
            DefaultSettings();

        }

    }
}
