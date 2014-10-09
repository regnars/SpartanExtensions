using System;
using System.Configuration;

namespace SpartanExtensions
{
    /// <summary>
    /// Abstract class for extending the configuration manager.
    /// </summary>
    public abstract class ConfigurationManagerExtensions
    {
        /// <summary>
        /// Gets the appsettings key value. Throws an exception if key is not present in .config file.
        /// </summary>
        public string GetAppKeyValue(string appKey)
        {
            var appKeyValue = ConfigurationManager.AppSettings[appKey];

            if (String.IsNullOrEmpty(appKeyValue))
                throw new Exception(string.Format("Please specify app key \"{0}\" in your application's .config file.", appKey));

            return appKeyValue;
        }
    }
}
