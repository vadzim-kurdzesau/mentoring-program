using System;
using System.Runtime.CompilerServices;

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

        public ConfigurationComponentBaseAttribute(string settingName, [CallerMemberName] string propertyName = null)
        {
            if (string.IsNullOrWhiteSpace(settingName))
            {
                throw new ArgumentNullException(nameof(settingName));
            }

            SettingName = settingName;
            Console.WriteLine(propertyName ?? "No value specified");
        }

        /// <summary>
        /// Gets the value on specified setting key from configuration.
        /// </summary>
        public abstract string GetValue();

        /// <summary>
        /// Sets the value on specified setting key to configuration.
        /// </summary>
        public abstract void SetValue(string value);
    }
}
