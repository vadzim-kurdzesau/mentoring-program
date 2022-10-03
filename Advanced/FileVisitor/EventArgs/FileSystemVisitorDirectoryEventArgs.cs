using System;
using System.IO;

namespace FileVisitor
{
    public class FileSystemVisitorDirectoryEventArgs : EventArgs
    {
        public DirectoryInfo DirectoryInfo { get; set; }
    }
}
