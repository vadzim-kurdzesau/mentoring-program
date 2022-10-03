using System;
using System.Collections.Generic;
using System.IO;

namespace FileVisitor
{
    public class FileSystemVisitor : IFileSystemVisitor
    {
        private readonly Predicate<FileSystemInfo> _filter;

        public FileSystemVisitor()
        {
        }

        public FileSystemVisitor(Predicate<FileSystemInfo> filter)
        {
            _filter = filter ?? throw new ArgumentNullException(nameof(filter));
        }

        public IEnumerable<FileSystemInfo> GetDirectoryContent(string directoryPath)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
                throw new ArgumentNullException($"'{nameof(directoryPath)}' cannot be null or whitespace.", nameof(directoryPath));

            if (!Directory.Exists(directoryPath))
                throw new ArgumentException($"Directory {directoryPath} does not exist!", nameof(directoryPath));

            return IterateThroughDirectories(directoryPath);
        }

        private IEnumerable<FileSystemInfo> IterateThroughDirectories(string parentDirectory)
        {
            var childDirectoriesNames = Directory.GetDirectories(parentDirectory);
            foreach (var directoryName in childDirectoriesNames)
            {
                foreach (var file in IterateThroughFilteredDirectories(directoryName))
                {
                    yield return file;
                }
            }

            var directoryFilesNames = Directory.GetFiles(parentDirectory);
            foreach (var fileName in directoryFilesNames)
            {
                var file = new FileInfo(fileName);
                if (_filter != null && !_filter(file))
                {
                    continue;
                }

                yield return file;
            }
        }

        private IEnumerable<FileSystemInfo> IterateThroughFilteredDirectories(string parentDirectory)
        {
            var directory = new DirectoryInfo(parentDirectory);
            if (_filter != null && !_filter(directory))
            {
                yield break;
            }

            yield return directory;

            var nextFiles = IterateThroughDirectories(parentDirectory);
            foreach (var nextFile in nextFiles)
            {
                yield return nextFile;
            }
        }
    }
}
