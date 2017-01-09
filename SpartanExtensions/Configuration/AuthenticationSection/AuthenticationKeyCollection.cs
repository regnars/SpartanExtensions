using System.Configuration;

namespace SpartanExtensions.Configuration.AuthenticationSection
{
    public class AuthenticationKeyCollection : ConfigurationElementCollection
    {
        public AuthenticationKey this[int index]
        {
            get
            {
                return (AuthenticationKey)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);
                BaseAdd(index, value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new AuthenticationKey();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((AuthenticationKey)element).Name;
        }
    }
}
