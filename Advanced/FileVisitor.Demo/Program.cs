using System;
using System.Linq;

namespace FileVisitor.Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                throw new ArgumentException("Provide path to the root directory.");
            }

            var fileSystemVisitor = new FileSystemVisitor(i => i.Name.Equals(".git"));
            fileSystemVisitor.Started += (_, _) => Console.WriteLine("Search started.");
            fileSystemVisitor.Finished += (_, _) => Console.WriteLine("Search finished.");

            fileSystemVisitor.FileFound += OnFileFound;
            fileSystemVisitor.DirectoryFound += OnDirectoryFound;

            var content = fileSystemVisitor.GetDirectoryContent(args[0]);

            var result = content.ToList();
        }

        private static void OnDirectoryFound(object sender, FileSystemVisitorDirectoryEventArgs eventArgs, ref bool abort, ref bool exclude)
        {
            if (eventArgs.DirectoryInfo.Name.Equals(".vs"))
            {
                exclude = true;
                Console.WriteLine($"Excluded directory: '{eventArgs.DirectoryInfo.FullName}'");
                return;
            }

            Console.WriteLine($"Found file: '{eventArgs.DirectoryInfo.FullName}'");
        }

        private static void OnFileFound(object sender, FileSystemVisitorFileEventArgs eventArgs, ref bool abort, ref bool exclude)
        {
            Console.WriteLine($"Found file: '{eventArgs.FileInfo.FullName}'");
        }
    }
}