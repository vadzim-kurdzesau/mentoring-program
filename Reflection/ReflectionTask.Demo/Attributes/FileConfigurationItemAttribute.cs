using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ReflectionTask.Demo.Attributes
{
    /// <summary>
    /// Sets a value from the JSON configuration file to the decorated property.
    /// </summary>
    public class FileConfigurationItemAttribute : ConfigurationComponentBaseAttribute
    {
        private static readonly JsonSerializerOptions serializerOptions = new()
        {
            WriteIndented = true,
        };

        /// <summary>
        /// Gets the path to configuration file.
        /// </summary>
        public string ConfigurationFilePath { get; }

        public override ConfigurationProviderType ProviderType => ConfigurationProviderType.Manager;

        public FileConfigurationItemAttribute(string configFilePath, string settingName)
            : base(settingName)
        {
            if (string.IsNullOrWhiteSpace(configFilePath))
            {
                throw new ArgumentNullException(nameof(configFilePath));
            }

            ConfigurationFilePath = configFilePath;
        }

        public string LoadSettings()
        {
            if (!File.Exists(ConfigurationFilePath))
            {
                throw new FileNotFoundException($"Could not find the file '{ConfigurationFilePath}'.");
            }

            var configuration = GetConfiguration();
            if (!configuration.TryGetValue(SettingName, out var value))
            {
                throw new ArgumentException($"Configuration does not contain the '{SettingName}' setting.");
            }

            return value;
        }

        public void SaveSettings(string value)
        {
            var configuration = GetConfiguration();
            if (!configuration.ContainsKey(SettingName))
            {
                throw new ArgumentException($"Configuration does not contain the '{SettingName}' setting.");
            }

            configuration[SettingName] = value;
            var serializedConfiguration = JsonSerializer.Serialize(configuration, serializerOptions);

            using (var fileStream = new FileStream(ConfigurationFilePath, FileMode.Create))
            {
                using (var streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.Write(serializedConfiguration);
                }
            }
        }

        private Dictionary<string, string> GetConfiguration()
        {
            using (var fileStream = new FileStream(ConfigurationFilePath, FileMode.Open, FileAccess.Read))
            {
                return JsonSerializer.Deserialize<Dictionary<string, string>>(fileStream, serializerOptions);
            }
        }
    }
}