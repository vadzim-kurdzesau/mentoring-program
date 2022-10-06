namespace ReflectionTask.Demo.Attributes
{
    public class ConfigurationManagerConfigurationItemAttribute : ConfigurationComponentBaseAttribute
    {
        public ConfigurationManagerConfigurationItemAttribute(string settingName)
            : base(settingName)
        {
        }
    }
}
