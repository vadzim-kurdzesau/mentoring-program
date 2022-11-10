using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ReflectionTask.Demo.Attributes;

namespace ReflectionTask.Demo
{
    public class Configuration
    {
        private const string PluginPattern = "*.dll";

        private Dictionary<ConfigurationProviderType, IConfigurationProvider> _configurationProviders = new();
        private readonly string _pluginDirectoryPath;

        public Configuration(string pluginDirectoryPath)
        {
            if (string.IsNullOrWhiteSpace(pluginDirectoryPath))
            {
                throw new ArgumentNullException(nameof(pluginDirectoryPath));
            }

            if (!Directory.Exists(pluginDirectoryPath))
            {
                throw new ArgumentException($"Directory '{pluginDirectoryPath}' does not exist!");
            }

            _pluginDirectoryPath = pluginDirectoryPath;
        }

        [ConfigurationManagerConfigurationItem("Username")]
        public string Username { get; set; }

        [ConfigurationManagerConfigurationItem("Password")]
        public string Password { get; set; }

        public void LoadSettings()
        {
            IterateThroughProperties((provider, property, attribute) =>
            {
                property.SetValue(this, provider.LoadSetting(attribute.SettingName));
            });
        }

        public void SaveSettings()
        {
            IterateThroughProperties((provider, property, attribute) =>
            {
                provider.SaveSetting(attribute.SettingName, property.GetValue(this).ToString());
            });
        }

        private void IterateThroughProperties(Action<IConfigurationProvider, PropertyInfo, ConfigurationComponentBaseAttribute> action)
        {
            foreach (var property in typeof(Configuration).GetProperties())
            {
                var attribute = Attribute.GetCustomAttribute(property, typeof(ConfigurationManagerConfigurationItemAttribute), false)
                    as ConfigurationManagerConfigurationItemAttribute;

                if (attribute != null)
                {
                    if (!_configurationProviders.TryGetValue(attribute.ProviderType, out var provider))
                    {
                        provider = LoadProvider(attribute.ProviderType);
                    }

                    action(provider, property, attribute);
                }
            }
        }

        private IConfigurationProvider LoadProvider(ConfigurationProviderType providerType)
        {
            var pluginsPaths = Directory.EnumerateFiles(_pluginDirectoryPath, PluginPattern);

            var provider = pluginsPaths.Select(pluginPath =>
            {
                Assembly pluginAssembly = LoadPlugin(pluginPath);
                return CreateProvider(pluginAssembly);
            }).FirstOrDefault(p => p.Type == providerType);

            if (provider is null)
            {
                throw new ArgumentException($"There is no plugin with the '{providerType}' type.");
            }

            return provider;
        }

        private static IConfigurationProvider CreateProvider(Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {
                if (typeof(IConfigurationProvider).IsAssignableFrom(type))
                {
                    if (Activator.CreateInstance(type) is IConfigurationProvider result)
                    {
                        return result;
                    }
                }
            }

            throw new ArgumentException($"There are no configuration providers in '{assembly.FullName}' assembly.");
        }

        private static Assembly LoadPlugin(string pluginPath)
        {
            var loadContext = new PluginLoadContext(pluginPath);
            return loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(pluginPath)));
        }
    }
}
