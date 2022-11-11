namespace ReflectionTask.Demo.Attributes
{
    /// <summary>
    /// Sets a value from the JSON configuration file to the decorated property.
    /// </summary>
    public class FileConfigurationItemAttribute : ConfigurationComponentBaseAttribute
    {
        /// <summary>
        /// Gets the path to configuration file.
        /// </summary>
        public string ConfigurationFilePath { get; }

        public override ConfigurationProviderType ProviderType => ConfigurationProviderType.File;

        public FileConfigurationItemAttribute(string configFilePath, string settingName)
            : base(settingName)
        {
            ConfigurationFilePath = configFilePath;
        }
    }
}