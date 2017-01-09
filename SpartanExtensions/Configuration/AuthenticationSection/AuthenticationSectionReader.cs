using System.Configuration;

namespace SpartanExtensions.Configuration.AuthenticationSection
{
    public class AuthenticationSectionReader
    {
        public AuthenticationSection AuthenticationSection { get; private set; }

        public AuthenticationSectionReader()
        {
            AuthenticationSection = ConfigurationManager.GetSection("authentication") as AuthenticationSection;
        }
    }
}
