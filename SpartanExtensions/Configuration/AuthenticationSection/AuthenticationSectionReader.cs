namespace SpartanExtensions.Configuration.AuthenticationSection
{
    public class AuthenticationSectionReader
    {
        public AuthenticationSection AuthenticationSection { get; private set; }

        public AuthenticationSectionReader()
        {
            AuthenticationSection = System.Configuration.ConfigurationManager.GetSection("authentication") as AuthenticationSection;
        }
    }
}
