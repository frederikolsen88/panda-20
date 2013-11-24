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
