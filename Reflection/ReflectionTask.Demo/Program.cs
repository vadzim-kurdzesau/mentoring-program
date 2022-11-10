using System;

namespace ReflectionTask.Demo
{
    internal class Program
    {
        private static void Main()
        {
            var configuration = new Configuration(@"C:\Users\Vadzim_Kurdzesau\source\repos\Learning\MentoringProgram\Reflection\ReflectionTask.Demo\Plugins");

            configuration.LoadSettings();

            configuration.Username = "Vadzim";

            configuration.SaveSettings();

            Console.WriteLine($"{configuration.Username}: {configuration.Password}");
        }
    }
}