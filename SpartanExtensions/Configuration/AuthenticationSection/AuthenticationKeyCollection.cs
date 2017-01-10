using System.Configuration;

namespace SpartanExtensions.Configuration.AuthenticationSection
{
    public class AuthenticationKeyCollection : ConfigurationElementCollection
    {
        public new AuthenticationKey this[string key]
        {
            get
            {
                return (AuthenticationKey)BaseGet(key);
            }
            set
            {
                var removableKey = BaseGet(key);
                if (removableKey != null)
                    BaseRemove(removableKey);
                BaseAdd(value);
            }
        }

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
