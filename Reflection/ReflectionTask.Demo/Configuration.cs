using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ReflectionTask.Demo.Attributes;
using ReflectionTask.Demo.Exceptions;

namespace ReflectionTask.Demo
{
    public class Configuration
    {
        private const string PluginPattern = "*.dll";

        private readonly Dictionary<ConfigurationProviderType, IConfigurationProvider> _configurationProviders = new();
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

        [FileConfigurationItem(@"C:\Users\Vadzim_Kurdzesau\source\repos\Learning\MentoringProgram\Reflection\ReflectionTask.Demo\appsettings.json", "Age")]
        public string Age { get; set; }

        [FileConfigurationItem(@"C:\Users\Vadzim_Kurdzesau\source\repos\Learning\MentoringProgram\Reflection\ReflectionTask.Demo\appsettings.json", "Balance")]
        public string Balance { get; set; }

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
                var attribute = Attribute.GetCustomAttribute(property, typeof(ConfigurationComponentBaseAttribute), false)
                    as ConfigurationComponentBaseAttribute;

                if (attribute != null)
                {
                    if (!_configurationProviders.TryGetValue(attribute.ProviderType, out var provider))
                    {
                        if (attribute.ProviderType == ConfigurationProviderType.File)
                        {
                            provider = LoadProvider(attribute.ProviderType, (attribute as FileConfigurationItemAttribute)?.ConfigurationFilePath);
                        }
                        else
                        {
                            provider = LoadProvider(attribute.ProviderType);
                        }

                        _configurationProviders.Add(attribute.ProviderType, provider);
                    }

                    action(provider, property, attribute);
                }
            }
        }

        private IConfigurationProvider LoadProvider(ConfigurationProviderType providerType, params object[] arguments)
        {
            var pluginsPaths = Directory.EnumerateFiles(_pluginDirectoryPath, PluginPattern);

            var provider = pluginsPaths.Select(pluginPath =>
            {
                Assembly pluginAssembly = LoadPlugin(pluginPath);
                return CreateProvider(pluginAssembly, arguments);
            }).FirstOrDefault(p => p != null && p.Type == providerType);

            if (provider is null)
            {
                throw new ConfigurationProviderException($"There is no plugin with the '{providerType}' type.");
            }

            return provider;
        }

        private static IConfigurationProvider CreateProvider(Assembly assembly, params object[] arguments)
        {
            foreach (var type in assembly.GetTypes())
            {
                if (typeof(IConfigurationProvider).IsAssignableFrom(type))
                {
                    var constructorInfo = type.GetConstructor(arguments.Select(a => a.GetType()).ToArray());
                    if (constructorInfo == null)
                    {
                        continue;
                    }

                    if (Activator.CreateInstance(type, arguments) is IConfigurationProvider result)
                    {
                        return result;
                    }
                }
            }

            return null;
        }

        private static Assembly LoadPlugin(string pluginPath)
        {
            var loadContext = new PluginLoadContext(pluginPath);
            return loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(pluginPath)));
        }
    }
}
