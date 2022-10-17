using ReflectionTask.Demo;
using System.Configuration;

namespace ReflectionTask.ConfigurationManagerProvider
{
    /// <summary>
    /// Sets a value to decorated property from the configuration manager.
    /// </summary>
    public class ConfigurationManagerConfigurationItemAttribute : ConfigurationComponentBaseAttribute
    {
        public ConfigurationManagerConfigurationItemAttribute(string settingName)
            : base(settingName)
        {
        }

        public override string LoadSettings()
        {
            return ConfigurationManager.AppSettings[SettingName];
        }

        public override void SaveSettings(string value)
        {
            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configuration.AppSettings.Settings;

            settings[SettingName].Value = value;
            configuration.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configuration.AppSettings.SectionInformation.Name);
        }
    }
}