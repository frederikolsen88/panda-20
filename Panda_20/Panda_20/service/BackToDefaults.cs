using System;
using System.Collections.Generic;
using System.Linq;
using Panda_20.Properties;

namespace Panda_20.service
{
    static class BackToDefaults
    {

        public static void DefaultSettings()
        {
            //TODO metode der resetter værdierne i Settings.Default

            Settings.Default.allow_notifications_comments_from_others = true;
            Settings.Default.allow_notifications_comments_from_self = false;
            Settings.Default.allow_notifications_comments_on_self_posts = false;
            Settings.Default.allow_notifications_posts_from_others = true;
            Settings.Default.allow_notifications_posts_from_self = false;

            Settings.Default.notifications_max_amount = 4;

        }

        public static void DefaultFacebookValues()
        {
            Settings.Default.fb_token = "";
            Settings.Default.fb_token_expires_in = "";
        }

        public static void ResetEverything()
        {
            DefaultFacebookValues();
            DefaultSettings();

        }

    }
}
