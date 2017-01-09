using System.Configuration;

namespace SpartanExtensions.Configuration.AuthenticationSection
{
    public class AuthenticationSection : ConfigurationSection
    {
        [ConfigurationProperty("authentication_keys", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(AuthenticationKeyCollection),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public AuthenticationKeyCollection Keys
        {
            get
            {
                return (AuthenticationKeyCollection)base["authentication_keys"];
            }
        }
    }
}
