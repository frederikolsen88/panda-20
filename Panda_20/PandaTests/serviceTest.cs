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
        public void ReadXmlValue_canRead()
        {
            String value = Service.ReadXmlValue("test", @"service\AppValues.xml");
            Assert.AreEqual("it seems to work", value);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Element <" + "shouldFail" + "> not found in XML-file <" + @"service\AppValues.xml" +">.")]
        public void ReadXmlValue_throwsException()
        {
            Service.ReadXmlValue("shouldFail", @"service\AppValues.xml");
        }



    }
}
