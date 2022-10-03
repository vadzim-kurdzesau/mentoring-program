using System;
using System.Linq;

namespace FileVisitor.Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //if (args.Length != 1)
            //    throw new ArgumentException("Provide path to the root directory.");

            var directoryPath = "C:\\Users\\Vadzim_Kurdzesau\\source\\repos\\Learning\\MentoringProgram\\Fundamentals" /*args[0]*/;
            var fileSystemVisitor = new FileSystemVisitor();
            fileSystemVisitor.Started += (_, _) => Console.WriteLine("Search started.");
            fileSystemVisitor.Finished += (_, _) => Console.WriteLine("Search finished.");
            fileSystemVisitor.DirectoryFound += OnDirectoryFound;
            fileSystemVisitor.FileFound += OnFileFound;
            var content = fileSystemVisitor.GetDirectoryContent(directoryPath);

            var result = content.ToList();
        }

        private static void OnDirectoryFound(object sender, FileSystemVisitorDirectoryEventArgs eventArgs, ref bool abort, ref bool exclude)
        {
            exclude = true;
            Console.WriteLine($"Excluded directory: '{eventArgs.DirectoryInfo.FullName}'");
        }

        private static void OnFileFound(object sender, FileSystemVisitorFileEventArgs eventArgs, ref bool abort, ref bool exclude)
        {
            Console.WriteLine($"Found file: '{eventArgs.FileInfo.FullName}'");
        }
    }
}