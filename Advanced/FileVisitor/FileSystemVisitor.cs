using System;
using System.Collections.Generic;
using System.IO;

namespace FileVisitor
{
    public delegate void FileSystemVisitorFileFoundEventHandler(object sender, FileSystemVisitorFileEventArgs eventArgs, ref bool abort, ref bool exclude);

    public delegate void FileSystemVisitorDirectoryFoundEventHandler(object sender, FileSystemVisitorDirectoryEventArgs eventArgs, ref bool abort, ref bool exclude);

    public class FileSystemVisitor : IFileSystemVisitor
    {
        private readonly Predicate<FileSystemInfo> _filter;
        private bool _isAborted;

        public FileSystemVisitor()
        {
        }

        public FileSystemVisitor(Predicate<FileSystemInfo> filter)
        {
            _filter = filter ?? throw new ArgumentNullException(nameof(filter));
        }

        /// <summary>
        /// Event that triggered when the directory traverse starts.
        /// </summary>
        public event EventHandler Started;

        /// <summary>
        /// Event that triggered when the directory traverse is finished.
        /// </summary>
        public event EventHandler Finished;

        /// <summary>
        /// Event that is triggered when a file is found.
        /// </summary>
        public event FileSystemVisitorFileFoundEventHandler FileFound;

        /// <summary>
        /// Event that is triggered when a directory is found.
        /// </summary>
        public event FileSystemVisitorDirectoryFoundEventHandler DirectoryFound;

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="directoryPath"/> is null, empty or whitespace.</exception>
        /// <exception cref="ArgumentException">Thrown if directoty <paramref name="directoryPath"/> does not exitst.</exception>
        public IEnumerable<FileSystemInfo> GetDirectoryContent(string directoryPath)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
                throw new ArgumentNullException($"'{nameof(directoryPath)}' cannot be null or whitespace.", nameof(directoryPath));

            if (!Directory.Exists(directoryPath))
                throw new ArgumentException($"Directory '{directoryPath}' does not exist.", nameof(directoryPath));

            OnStarted();
            foreach (var file in IterateThroughDirectories(directoryPath))
            {
                if (_isAborted)
                {
                    yield break;
                }

                yield return file;
            }

            OnFinished();
        }

        /// <summary>
        /// Method that called when the directory traverse starts.
        /// </summary>
        protected virtual void OnStarted()
        {
            Started?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Method that called when the directory traverse is finished.
        /// </summary>
        protected virtual void OnFinished()
        {
            Finished?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Method that is called when a file is found.
        /// </summary>
        protected virtual void OnFileFound(FileInfo fileInfo, ref bool abort, ref bool exclude)
        {
            FileFound?.Invoke(this, new FileSystemVisitorFileEventArgs { FileInfo = fileInfo }, ref abort, ref exclude);
        }

        /// <summary>
        /// Method that is called when a directory is found.
        /// </summary>
        protected virtual void OnDirectoryFound(DirectoryInfo directoryInfo, ref bool abort, ref bool exclude)
        {
            DirectoryFound?.Invoke(this, new FileSystemVisitorDirectoryEventArgs { DirectoryInfo = directoryInfo }, ref abort, ref exclude);
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
                var isExcluded = false;

                OnFileFound(file, ref _isAborted, ref isExcluded);
                if (isExcluded || (_filter != null && !_filter(file)))
                {
                    continue;
                }

                yield return file;
            }
        }

        private IEnumerable<FileSystemInfo> IterateThroughFilteredDirectories(string parentDirectory)
        {
            var directory = new DirectoryInfo(parentDirectory);
            var isExcluded = false;

            OnDirectoryFound(directory, ref _isAborted, ref isExcluded);
            if (isExcluded || (_filter != null && !_filter(directory)))
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
