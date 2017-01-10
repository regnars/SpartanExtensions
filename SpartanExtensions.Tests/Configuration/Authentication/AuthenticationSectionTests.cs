using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpartanExtensions.Configuration.AuthenticationSection;

namespace SpartanExtensions.Tests.Configuration.Authentication
{
    [TestClass]
    public class AuthenticationSectionTests
    {
        [TestMethod]
        public void GettingAuthenticationKeyByItsIndexRepresentativeShouldPass()
        {
            Assert.AreEqual(new AuthenticationSectionReader().AuthenticationSection.Keys[0].Name,
                "typefi");
        }

        [TestMethod]
        public void GettingAuthenticationKeyByItsStringRepresentativeShouldPass()
        {
            Assert.AreEqual(new AuthenticationSectionReader().AuthenticationSection.Keys["typefi"].Name,
               "typefi");
        }
    }
}
