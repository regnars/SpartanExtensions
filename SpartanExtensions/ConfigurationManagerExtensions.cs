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

            if (string.IsNullOrEmpty(appKeyValue))
                throw new Exception($"Please specify app key \"{appKey}\" in your application's .config file.");

            return appKeyValue;
        }
    }
}
