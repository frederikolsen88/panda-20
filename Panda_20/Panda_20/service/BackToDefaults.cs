using System;
using System.Collections.Generic;
using System.Linq;
using Panda_20.Properties;

namespace Panda_20.service
{
    /**
     * Class for handling default settings.
     * 
     * Author: Tobias Roland Rasmussen
     */
    static class BackToDefaults
    {

        // Instance of the settings-file.
        private static readonly Settings Sd = Settings.Default;

        // Restores all settings to their defaults and saves the settings.
        public static void DefaultSettings()
        {
            // COMMENTS
            Sd.comments_display_comments_on_own_post = false;
            Sd.comments_display_notifications = true;
            Sd.comments_display_own = false;
            Sd.comments_play_sound = true;
            Sd.comments_time_limit = 5;
            Sd.comments_time_popdown_enabled = false;
            Sd.comments_time_popdown = 5;

            // MESSAGES
            Sd.pm_display_notifications = true;
            Sd.pm_play_sound = true;
            Sd.pm_time_limit = 5;
            Sd.pm_time_popdown_enabled = false;
            Sd.pm_time_popdown = 10;
            Sd.pm_display_own = false;
            
            // NOTIFICATIONS GENERALLY
            Sd.notifications_max_amount = 4;
            Sd.notifications_play_sound = true;
            Sd.notifactions_time_limit = 5;
            Sd.colour_warnings = true;

            // POSTS
            Sd.posts_display_notifications = true;
            Sd.posts_display_own = false;
            Sd.posts_play_sound = true;
            Sd.posts_time_limit = 5;
            Sd.posts_time_popdown_enabled = false;
            Sd.posts_time_popdown = 15;

            // STARTUP
            Sd.start_with_windows = false;
            Service.ConfigureRegistryKeyForStartup(false);

            Sd.Save();
        }

        // Clears the user's Facebook credentials.
        public static void DefaultFacebookValues()
        {
            Sd.fb_token = "";
            Sd.fb_token_expires_in = "";
            Sd.Save();
        }

        // Clears the user's Facebook credentials and restores all settings to their defaults.
        public static void ResetEverything()
        {
            DefaultFacebookValues();
            DefaultSettings();
        }
    }
}
