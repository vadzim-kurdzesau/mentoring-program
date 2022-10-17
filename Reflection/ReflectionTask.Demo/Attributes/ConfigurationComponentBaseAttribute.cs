using System;

namespace ReflectionTask.Demo.Attributes
{
    /// <summary>
    /// Sets a value from the configuration to the decorated property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class ConfigurationComponentBaseAttribute : Attribute
    {
        /// <summary>
        /// Gets the name of a setting in configuration.
        /// </summary>
        public string SettingName { get; }

        public ConfigurationComponentBaseAttribute(string settingName)
        {
            if (string.IsNullOrWhiteSpace(settingName))
            {
                throw new ArgumentNullException(nameof(settingName));
            }

            SettingName = settingName;
        }

        /// <summary>
        /// Loads the value on specified setting key from configuration.
        /// </summary>
        public abstract string LoadSettings();

        /// <summary>
        /// Saves the <paramref name="value"/> on specified setting key to configuration.
        /// </summary>
        public abstract void SaveSettings(string value);
    }
}
