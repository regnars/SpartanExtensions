using System;

namespace SpartanExtensions
{
    /// <summary>
    /// Wrapping ConfigurationManager functionality with additional checks
    /// </summary>
    public static class ConfigurationManager
    {
        /// <summary>
        /// Gets the values associated with the specified key from the System.Collections.Specialized.NameValueCollection
        /// combined into one comma-separated list.
        /// </summary>
        /// <param name="key">The System.String key of the entry that contains the values to get.</param>
        /// <returns> 
        /// A System.String that contains a comma-separated list of the values associated
        /// with the specified key from the System.Collections.Specialized.NameValueCollection,
        /// if found; otherwise, throws exception.
        /// </returns>
        /// <exception cref="Exception">
        /// Please specify app key \"{key}\" in your application's .config file.
        /// </exception>
        public static string GetAppSettingStringOrException(string key)
        {
            var value = System.Configuration.ConfigurationManager.AppSettings.Get(key);

            if (string.IsNullOrEmpty(value))
            {
                throw new Exception($"Please specify app key \"{key}\" in your application's .config file.");
            }

            return value;
        }

        /// <summary>
        /// Gets the values associated with the specified key from the SystemConfiguration as System.String
        /// </summary>
        /// <param name="key">The System.String key of the entry that contains the value to get.</param>
        /// <returns>The string value assigned to the System.Configuration.ConnectionStringSettings.ConnectionString property.</returns>
        /// <exception cref="Exception">Please specify connection string \"{key}\" in your application's .config file.</exception>
        public static string GetConnectionStringOrException(string key)
        {
            var value = System.Configuration.ConfigurationManager.ConnectionStrings[key]?.ConnectionString;

            if (string.IsNullOrEmpty(value))
            {
                throw new Exception($"Please specify connection string \"{key}\" in your application's .config file.");
            }

            return value;
        }
    }
}
