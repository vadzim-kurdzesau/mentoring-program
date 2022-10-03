using System;

namespace FileVisitor.Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 1)
                throw new ArgumentException("Provide path to the root directory.");

            var directoryPath = args[0];
            var fileSystemVisitor = new FileSystemVisitor();
            var content = fileSystemVisitor.GetDirectoryContent(directoryPath);

            foreach (var item in content)
            {
                Console.WriteLine(item.FullName);
            }
        }
    }
}