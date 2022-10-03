using System.Collections.Generic;
using System.IO;

namespace FileVisitor
{
    public interface IFileSystemVisitor
    {
        /// <summary>
        /// Returns all <paramref name="parentDirectory"/> content including subdirectories content.
        /// </summary>
        /// <param name="parentDirectory">Path to root directory.</param>
        IEnumerable<FileSystemInfo> GetDirectoryContent(string parentDirectory);
    }
}
