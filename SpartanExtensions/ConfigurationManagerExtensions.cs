using System;
using System.Configuration;

namespace SpartanExtensions
{
    public abstract class ConfigurationManagerExtensions
    {
        public string GetAppKeyValue(string appKey)
        {
            var appKeyValue = ConfigurationManager.AppSettings[appKey];

            if (String.IsNullOrEmpty(appKeyValue))
                throw new Exception(string.Format("Please specify app key \"{0}\" in your application's .config file.", appKey));

            return appKeyValue;
        }
    }
}
