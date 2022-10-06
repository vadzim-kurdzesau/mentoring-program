namespace ReflectionTask.Demo.Attributes
{
    public class FileConfigurationItemAttribute : ConfigurationComponentBaseAttribute
    {
        public string ConfigFilePath { get; }

        public FileConfigurationItemAttribute(string configFilePath, string settingName)
            : base(settingName)
        {
            ConfigFilePath = configFilePath;
        }
    }
}
