using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpartanExtensions.Tests.StringExtensions.Mock;
using System.Xml.Serialization;

namespace SpartanExtensions.Tests.StringExtensions
{
    [TestClass]
    public class StringExtensionTests
    {
        [TestMethod]
        public void ToEnumBySpecifiedAttributeValueShouldGetExistingCaseInsensitiveEnumValue()
        {
            var type = "tYpe-TwO".ToEnumBySpecifiedAttributeValue<Types, XmlEnumAttribute, string>(attribute => attribute.Name);
            Assert.IsTrue(type == Types.TypeTwo);
        }

        [TestMethod]
        public void ToEnumBySpecifiedAttributeValueShouldGetExistingEnumValue()
        {
            var type = "type-two".ToEnumBySpecifiedAttributeValue<Types, XmlEnumAttribute, string>(attribute => attribute.Name);
            Assert.IsTrue(type == Types.TypeTwo);
        }

        [TestMethod]
        public void ToEnumBySpecifiedAttributeValueShouldNotGetUnexistingEnumValue()
        {
            var type = "xxx".ToEnumBySpecifiedAttributeValue<Types, XmlEnumAttribute, string>(attribute => attribute.Name);
            Assert.IsTrue(type == null);
        }

        [TestMethod]
        public void TransformingStringIntoSecureStringAndBackShouldFunctionCorrectly()
        {
            const string password = "admin";
            Assert.AreEqual(password, password.ToSecureString().ToPlainString());
        }
    }
}
