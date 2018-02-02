using System;
using System.Configuration;

namespace Petronas.SampleApp.Common.Helpers
{
    public static class ConfigurationHelper
    {
        public static string GetSetting(ConfigurationKey key)
        {
            string keyString = key.ToString();

            var value = GetConfigFromConfigFile(keyString);

            if (value == null)
            {
                throw new ArgumentOutOfRangeException("key", string.Format("Cannot find setting with key {0}", keyString));
            }

            return value;
        }

        private static string GetConfigFromConfigFile(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}