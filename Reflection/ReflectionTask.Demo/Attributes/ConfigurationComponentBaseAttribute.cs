using System;

namespace ReflectionTask.Demo.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class ConfigurationComponentBaseAttribute : Attribute
    {
        public string SettingName { get; }

        public ConfigurationComponentBaseAttribute(string settingName)
        {
            SettingName = settingName;
        }
    }
}
