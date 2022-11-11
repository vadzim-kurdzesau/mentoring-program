namespace ReflectionTask.Demo
{
    /// <summary>
    /// Loads and saves settings to the configuration provider of specified <see cref="ConfigurationProviderType"/> type.
    /// </summary>
    public interface IConfigurationProvider
    {
        /// <summary>
        /// Gets the type of this configuration provider.
        /// </summary>
        ConfigurationProviderType Type { get; }

        /// <summary>
        /// Loads value for the setting with the specified <paramref name="settingName"/>.
        /// </summary>
        string LoadSetting(string settingName);

        /// <summary>
        /// Saves the <paramref name="value"/> to setting with the specified <paramref name="settingName"/>.
        /// </summary>
        void SaveSetting(string settingName, string value);
    }
}
