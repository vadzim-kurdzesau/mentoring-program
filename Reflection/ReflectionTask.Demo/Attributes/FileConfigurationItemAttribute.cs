using System;
using System.IO;

namespace ReflectionTask.Demo.Attributes
{
    /// <summary>
    /// Sets a value from the JSON configuration file to the decorated property.
    /// </summary>
    public class FileConfigurationItemAttribute : ConfigurationComponentBaseAttribute
    {
        /// <summary>
        /// Gets the path to configuration file.
        /// </summary>
        public string ConfigurationFilePath { get; }

        public FileConfigurationItemAttribute(string configFilePath, string settingName)
            : base(settingName)
        {
            if (string.IsNullOrWhiteSpace(configFilePath))
            {
                throw new ArgumentNullException(nameof(configFilePath));
            }

            ConfigurationFilePath = configFilePath;
        }

        public override string GetValue()
        {
            if (!File.Exists(ConfigurationFilePath))
            {

            }

            throw new NotImplementedException();
        }

        public override void SetValue(string value)
        {
            throw new NotImplementedException();
        }
    }
}
