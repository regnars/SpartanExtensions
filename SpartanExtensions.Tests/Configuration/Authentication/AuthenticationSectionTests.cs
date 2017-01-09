using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpartanExtensions.Configuration.AuthenticationSection;

namespace SpartanExtensions.Tests.Configuration.Authentication
{
    [TestClass]
    public class AuthenticationSectionTests
    {
        [TestMethod]
        public void GettingAuthenticationSectionShouldPass()
        {
            Assert.AreEqual(new AuthenticationSectionReader().AuthenticationSection.Keys[0].Name,
                "typefi");
        }
    }
}
