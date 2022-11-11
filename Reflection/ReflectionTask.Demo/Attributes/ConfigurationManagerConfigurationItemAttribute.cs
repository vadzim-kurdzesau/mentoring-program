using ReflectionTask.Demo.Providers;

namespace ReflectionTask.Demo.Attributes
{
    public class ConfigurationManagerConfigurationItemAttribute : ConfigurationComponentBaseAttribute
    {
        public ConfigurationManagerConfigurationItemAttribute(string settingName)
            : base(settingName)
        {
        }

        public override ConfigurationProviderType ProviderType => ConfigurationProviderType.Manager;
    }
}