using Microsoft.Extensions.Configuration;
using ReflectionTask.Demo.Attributes;
using System.Reflection;
using System;

namespace ReflectionTask.Demo
{
    internal class Program
    {
        private static void Main()
        {
            var user = new UserInfo();
            foreach (var property in typeof(UserInfo).GetProperties())
            {
                var attribute = Attribute.GetCustomAttribute(property, typeof(FileConfigurationItemAttribute)) as FileConfigurationItemAttribute;

                if (attribute != null)
                {
                    var config = new ConfigurationBuilder()
                        .AddJsonFile(attribute.ConfigFilePath).Build();

                    user.Username = config[attribute.SettingName];
                }
            }

            Console.WriteLine(user.Username);
        }
    }

    public class UserInfo
    {
        [FileConfigurationItem(@"C:\Users\Vadzim_Kurdzesau\source\repos\Learning\MentoringProgram\Reflection\ReflectionTask.Demo\appsettings.json", "username")]
        public string Username { get; set; }
    }
}