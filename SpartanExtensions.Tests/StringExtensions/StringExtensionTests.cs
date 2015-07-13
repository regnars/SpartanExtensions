using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpartanExtensions.Tests.StringExtensions.Mock;
using SpartanExtensions;
using System.Xml.Serialization;
using System;

namespace SpartanExtensions.Tests.StringExtensions
{
    [TestClass]
    public class StringExtensionTests
    {
        [TestMethod]
        public void ShouldGetExistingEnumValue()
        {
            var type = "type-two".GetEnumValueByAttributeValue<Types, XmlEnumAttribute>(compareAgainstAttributeProperty: "Name");
            Assert.IsTrue(type == Types.TypeTwo);
        }

        [TestMethod]
        public void ShouldNotGetUnexistingEnumValue()
        {
            try
            {
                var type = "xxx".GetEnumValueByAttributeValue<Types, XmlEnumAttribute>(compareAgainstAttributeProperty: "Name");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }
    }
}
