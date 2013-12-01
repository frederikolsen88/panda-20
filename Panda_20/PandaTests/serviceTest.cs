
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Panda_20;
using Panda_20.Properties;
using Panda_20.service;


namespace PandaTests
{

    [TestClass]
    public class DefaultsTests
    {
        private static Settings sd = Settings.Default;

        [TestMethod]
        public void BackToDefaults_PositiveTest1()
        {
            
            // 1 - Changing something from a default setting:
            sd.pm_display_notifications = false;

            // 2 - Reset 
            BackToDefaults.DefaultSettings();

            // 3 - Assert the reset to default settings worked
            Assert.AreEqual(true, sd.pm_display_notifications);
        }

        [TestMethod]
        public void BackToDefaults_NegativeTest1()
        {
            // 1 - Changing something from a default setting:
            sd.pm_display_notifications = false;

            // 2 - No reset 

            // 3 - Assert that the change was actually due to the reset
            Assert.AreNotEqual(true, sd.pm_display_notifications);

            BackToDefaults.DefaultSettings();
        }

    }
}
