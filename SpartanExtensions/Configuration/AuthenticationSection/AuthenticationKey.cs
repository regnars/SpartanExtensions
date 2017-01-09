using System;
using System.ComponentModel;
using System.Configuration;
using System.Security;

namespace SpartanExtensions.Configuration.AuthenticationSection
{
    public class AuthenticationKey : ConfigurationElement
    {
        private const string ErrorMessageFormat =
            "Missing \"{0}\" attribute for authentication key in your application's .config file. Please check your \"authentication\" section.";

        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get
            {
                var name = this["name"];
                if (name == null)
                    throw new Exception(string.Format(ErrorMessageFormat, "name"));
                return name.ToString();
            }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("username", IsRequired = true)]
        public string Username
        {
            get
            {
                var username = this["username"];
                if (username == null)
                    throw new Exception(string.Format(ErrorMessageFormat, "username"));
                return username.ToString();
            }
            set { this["username"] = value; }
        }

        [TypeConverter(typeof(SecureStringTypeConverter))]
        [ConfigurationProperty("password", IsRequired = true, IsKey = false)]
        public SecureString Password
        {
            get
            {
                var password = this["password"];
                if (password == null)
                    throw new Exception(string.Format(ErrorMessageFormat, "password"));
                return password.ToString().ToSecureString();
            }
            set { this["password"] = value.ToPlainString(); }
        }
    }
}
