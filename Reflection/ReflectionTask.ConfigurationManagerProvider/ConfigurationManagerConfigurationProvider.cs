using ReflectionTask.Demo;
using System.Configuration;

namespace ReflectionTask.ConfigurationManagerProvider
{
    public class ConfigurationManagerConfigurationProvider : IConfigurationProvider
    {
        public ConfigurationProviderType Type => ConfigurationProviderType.Manager;

        public string LoadSetting(string settingName)
        {
            return ConfigurationManager.AppSettings[settingName]
                ?? throw new ArgumentException($"There is no setting with name '{settingName}'.");
        }

        public void SaveSetting(string settingName, string value)
        {
            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configuration.AppSettings.Settings;

            settings[settingName].Value = value;
            configuration.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configuration.AppSettings.SectionInformation.Name);
        }
    }
}
