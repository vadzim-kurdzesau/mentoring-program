using System;
using System.IO;

namespace FileVisitor
{
    public class FileSystemVisitorFileEventArgs : EventArgs
    {
        public FileInfo FileInfo { get; set; }
    }
}
