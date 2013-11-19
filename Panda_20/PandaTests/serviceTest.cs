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
        public void ReadXmlCanReadXml()
        {
            String value = Misc.GetXmlElement("test");
            Assert.AreEqual("it seems to work", value);
        }


    }
}
