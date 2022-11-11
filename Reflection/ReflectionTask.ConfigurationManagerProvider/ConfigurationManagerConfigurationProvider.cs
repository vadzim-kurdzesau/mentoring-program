using ReflectionTask.Demo;
using ReflectionTask.Demo.Exceptions;
using System.Configuration;

namespace ReflectionTask.ConfigurationManagerProvider
{
    /// <summary>
    /// Loads from and saves configuration to the configuration manager.
    /// </summary>
    public class ConfigurationManagerConfigurationProvider : IConfigurationProvider
    {
        public ConfigurationProviderType Type => ConfigurationProviderType.Manager;

        public string LoadSetting(string settingName)
        {
            return ConfigurationManager.AppSettings[settingName]
                ?? throw new ConfigurationProviderException($"There is no setting with the '{settingName}' name.");
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
