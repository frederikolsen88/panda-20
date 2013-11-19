using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Panda_20;
using Panda_20.service;


namespace PandaTests
{
    [TestClass]
    public class ServiceTest
    {
        [TestMethod]
        public void ReadXmlValue_canRead_appvalues()
        {
            String value = Service.ReadXmlValue("test", @"service\AppValues.xml");
            Assert.AreEqual("it seems to work", value);
        }

        [TestMethod]
        public void ReadXmlValue_canRead_settings()
        {
            String value = Service.ReadXmlValue("test", @"service\SettingAccess.xml");
            Assert.AreEqual("it works", value);
        }


        [TestMethod]
        [ExpectedException(typeof(Exception), "Element <" + "shouldFail" + "> not found in XML-file <" + @"service\AppValues.xml" +">.")]
        public void ReadXmlValue_throwsException()
        {
            Service.ReadXmlValue("shouldFail", @"service\AppValues.xml");
        }

        [TestMethod]
        public void WriteXmlValue()
        {
            String currentValue = Service.ReadXmlValue("test_write", @"service\AppValues.xml");
            String newValue = "on";
            // new value incremented, unless
            if (currentValue.Equals("on"))
            {
                newValue = "off";
            }

            Service.WriteXmlValue("test_write", newValue,  @"service\AppValues.xml");


            Assert.AreEqual(newValue, Service.ReadXmlValue("test_write", @"service\AppValues.xml"));
            

        }



    }
}
