using System.Configuration;
using System.Runtime.CompilerServices;

namespace ReflectionTask.Demo.Attributes
{
    /// <summary>
    /// Sets a value to decorated property from the configuration manager.
    /// </summary>
    public class ConfigurationManagerConfigurationItemAttribute : ConfigurationComponentBaseAttribute
    {
        public ConfigurationManagerConfigurationItemAttribute(string settingName, [CallerMemberName] string propertyName = null)
            : base(settingName, propertyName)
        {
        }

        public override string GetValue()
        {
            return ConfigurationManager.AppSettings[SettingName];
        }

        public override void SetValue(string value)
        {
            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configuration.AppSettings.Settings;

            settings[SettingName].Value = value;
            configuration.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configuration.AppSettings.SectionInformation.Name);
        }
    }
}
