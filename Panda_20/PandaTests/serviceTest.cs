using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Panda_20;


namespace PandaTests
{
    [TestClass]
    public class ServiceTest
    {
        [TestMethod]
        public void ReadXmlCanReadXml()
        {
            String value = Service.GetXmlElement("test");
            Assert.AreEqual("it seems to work", value);
        }


    }
}
