using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpartanExtensions.Tests.StringExtensions.Mock;
using System.Xml.Serialization;
using SpartanExtensions;
using System;

namespace SpartanExtensions.Tests.EnumExtensions
{
    [TestClass]
    public class EnumExtensionTests
    {
        [TestMethod]
        public void ToAttributeSpecifiedValueShouldGetExistingValue()
        {
            var type = Types.TypeTwo.ToAttributeSpecifiedValue<Types, XmlEnumAttribute, string>(attribute => attribute.Name);
            Assert.IsTrue(type.ToEnumBySpecifiedAttributeValue<Types, XmlEnumAttribute, string>(attribute => attribute.Name) == Types.TypeTwo);
        }
    }
}
