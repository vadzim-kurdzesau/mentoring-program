using ReflectionTask.Demo.Attributes;
using System;
using System.Configuration;

namespace ReflectionTask.Demo
{
    internal class Program
    {
        private static void Main()
        {
            var appSettings = ConfigurationManager.AppSettings;
            var cl = new MyClass();

            foreach (var item in appSettings.AllKeys)
            {
                Console.WriteLine(item);
            }

            foreach (var property in typeof(MyClass).GetProperties())
            {
                var attribute = Attribute.GetCustomAttribute(property, typeof(ConfigurationComponentBaseAttribute), true) as ConfigurationComponentBaseAttribute;
                if (attribute != null)
                {
                    Console.WriteLine(attribute.SettingName);
                }

                property.SetValue(cl, attribute.LoadSettings());
                attribute.SaveSettings("UPDATED");
            }
        }

        class MyClass
        {
            //[ConfigurationManagerConfigurationItem("Username")]
            [FileConfigurationItem(@"C:\Users\Vadzim_Kurdzesau\source\repos\Learning\MentoringProgram\Reflection\ReflectionTask.Demo\appsettings.json", "Username")]
            public string MyProperty { get; set; }
        }
    }
}