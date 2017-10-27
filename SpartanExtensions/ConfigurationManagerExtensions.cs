using System;

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
        /// <param name="appKey">The System.String key of the entry that contains the values to get.</param>
        /// <returns>
        /// A System.String that contains a comma-separated list of the values associated
        /// with the specified key from the System.Collections.Specialized.NameValueCollection,
        /// if found; otherwise, throws exception.
        /// </returns>
        /// /// <exception cref="Exception">
        /// Please specify app key \"{key}\" in your application's .config file.
        /// </exception>
        public string GetAppKeyValue(string appKey)
        {
            return ConfigurationManager.GetAppSettingStringOrException(appKey);
        }
    }
}
