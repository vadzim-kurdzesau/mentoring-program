using System;
using System.IO;
using System.Linq;
using Xunit;
using System.Collections.Generic;

namespace FileVisitor.Tests
{
    public class FileVisitorTests : IDisposable
    {
        private readonly string _rootFolderName;

        public FileVisitorTests()
        {
            _rootFolderName = $"FileVisitorTestFolder{Guid.NewGuid()}";
            Directory.CreateDirectory(_rootFolderName);
        }

        public void Dispose()
        {
            Directory.Delete(_rootFolderName, true);
        }

        [Fact]
        public void GetDirectoryContent_ReturnsAllDirectories()
        {
            // Arrange
            var expectedDirectories = new List<string> { "TestFolder1", "TestFolder2", "TestFolder3", "TestFolder4", "TestFolder5" };
            expectedDirectories.ForEach(d => CreateDirectory(d, _rootFolderName));

            var fileVisitor = new FileSystemVisitor();

            // Act
            var actual = fileVisitor.GetDirectoryContent(_rootFolderName);

            // Assert
            foreach (var directory in expectedDirectories)
            {
                Assert.NotNull(actual.Single(d => d.Name.Equals(directory)));
            }
        }

        [Fact]
        public void GetDirectoryContent_ReturnsAllFiles()
        {
            // Arrange
            var expectedFiles = new List<string> { "TestFile1", "TestFile2", "TestFile3", "TestFile4", "TestFile5" };
            expectedFiles.ForEach(f => CreateFile(f, _rootFolderName));

            var fileVisitor = new FileSystemVisitor();

            // Act
            var actual = fileVisitor.GetDirectoryContent(_rootFolderName);

            // Assert
            foreach (var directory in expectedFiles)
            {
                Assert.NotNull(actual.Single(d => d.Name.Equals(directory)));
            }
        }

        [Fact]
        public void GetDirectoryContent_ReturnsAllDirectoriesAndFilesInThem()
        {
            // Arrange
            var expectedFiles = new string[] { "TestFile1", "TestFile2", "TestFile3", "TestFile4", "TestFile5" };
            var expectedDirectories = new string[] { "TestFolder1", "TestFolder2", "TestFolder3", "TestFolder4", "TestFolder5" };
            for (int i = 0; i < expectedDirectories.Length; i++)
            {
                CreateDirectory(expectedDirectories[i], _rootFolderName);
                CreateFile(expectedFiles[i], _rootFolderName, expectedDirectories[i]);
            }

            var fileVisitor = new FileSystemVisitor();

            // Act
            var actual = fileVisitor.GetDirectoryContent(_rootFolderName).ToArray();

            // Assert (check the content order)
            for (int i = 0, j = 0; i < expectedDirectories.Length; i++, j++)
            {
                Assert.Equal(expectedDirectories[i], actual[j].Name);
                Assert.Equal(expectedFiles[i], actual[++j].Name);
            }
        }

        [Theory]
        [MemberData(nameof(Filters))]
        public void GetDirectoryContent_FilterProvided_ReturnsFilteredDirectoryContent(Predicate<FileSystemInfo> filter)
        {
            // Arrange
            var expectedFiles = new List<string> { "TestFile1.txt", "TestFile2", "TestFile3", "TestFile4Excluded", "TestFile5.txt", "TestFile6Excluded.txt" };
            expectedFiles.ForEach(f => CreateFile(f, _rootFolderName));

            var fileVisitor = new FileSystemVisitor(filter);

            // Act
            var actual = fileVisitor.GetDirectoryContent(_rootFolderName);
            var expectedPassedFilterFiles = expectedFiles.Select(f => new FileInfo(f)).Where(f => filter(f));
            var expectedNotPassedFilterFiles = expectedFiles.Select(f => new FileInfo(f)).Where(f => !filter(f));

            // Assert
            foreach (var file in expectedPassedFilterFiles)
            {
                Assert.NotNull(actual.Single(f => f.Name.Equals(file.Name)));
            }

            foreach (var file in expectedNotPassedFilterFiles)
            {
                Assert.DoesNotContain(actual, f => f.Name.Equals(file.Name));
            }
        }

        public static IEnumerable<object[]> Filters => new List<object[]>
        {
            new object[] { (Predicate<FileSystemInfo>)((FileSystemInfo i) => i.Extension.Equals(".txt")) },
            new object[] { (Predicate<FileSystemInfo>)((FileSystemInfo i) => i.Name.Contains("Exclude")) }
        };

        private static void CreateDirectory(string directoryName, params string[] path)
        {
            Directory.CreateDirectory(Path.Combine(Path.Combine(path), directoryName));
        }

        private static void CreateFile(string fileName, params string[] path)
        {
            File.Create(Path.Combine(Path.Combine(path), fileName)).Dispose();
        }
    }
}