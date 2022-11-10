using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionTask.Demo
{
    public interface IConfigurationProvider
    {
        ConfigurationProviderType Type { get; }

        string LoadSetting(string settingName);

        void SaveSetting(string settingName, string value);
    }
}
