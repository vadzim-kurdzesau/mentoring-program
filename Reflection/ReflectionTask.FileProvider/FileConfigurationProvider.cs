using System.Reflection;
using System.Text.Json;
using ReflectionTask.Demo;
using ReflectionTask.Demo.Exceptions;

namespace ReflectionTask.FileProvider
{
    /// <summary>
    /// Loads from and saves configuration to JSON file.
    /// </summary>
    public class FileConfigurationProvider : IConfigurationProvider
    {
        private static readonly JsonSerializerOptions serializerOptions = new()
        {
            WriteIndented = true,
        };

        public ConfigurationProviderType Type => ConfigurationProviderType.File;

        /// <summary>
        /// Gets the path to configuration file.
        /// </summary>
        public string ConfigurationFilePath { get; }
        
        public FileConfigurationProvider(string configFilePath)
        {
            if (string.IsNullOrWhiteSpace(configFilePath))
            {
                throw new ArgumentNullException(nameof(configFilePath));
            }

            if (!Path.IsPathRooted(configFilePath))
            {
                configFilePath = Path.Combine(
                    Directory.GetParent(
                        Directory.GetParent(
                            Assembly.GetExecutingAssembly().Location).FullName).FullName, configFilePath);
            }

            if (!File.Exists(configFilePath))
            {
                throw new ConfigurationProviderException($"There is no configuration file on '{configFilePath}' path.");
            }

            ConfigurationFilePath = configFilePath;
        }

        public string LoadSetting(string settingName)
        {
            if (!File.Exists(ConfigurationFilePath))
            {
                throw new FileNotFoundException($"Could not find the file '{ConfigurationFilePath}'.");
            }

            var configuration = GetConfiguration();
            if (!configuration.TryGetValue(settingName, out var value))
            {
                throw new ArgumentException($"Configuration does not contain the '{settingName}' setting.");
            }

            return value;
        }

        public void SaveSetting(string settingName, string value)
        {
            var configuration = GetConfiguration();
            if (!configuration.ContainsKey(settingName))
            {
                throw new ArgumentException($"Configuration does not contain the '{settingName}' setting.");
            }

            configuration[settingName] = value;
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
