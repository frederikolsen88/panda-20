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
            //arrange
            Service service = Service.Instance;


            String value = service.GetXmlElement("test");
            Assert.AreEqual("it seems to work", value);



        }
    }
}
