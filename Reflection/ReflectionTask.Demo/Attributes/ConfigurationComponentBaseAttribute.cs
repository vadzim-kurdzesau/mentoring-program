using System;
using ReflectionTask.Demo.Providers;

namespace ReflectionTask.Demo.Attributes
{
    /// <summary>
    /// Sets a value from the configuration to the decorated property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class ConfigurationComponentBaseAttribute : Attribute
    {
        public abstract ConfigurationProviderType ProviderType { get; }

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
    }
}
