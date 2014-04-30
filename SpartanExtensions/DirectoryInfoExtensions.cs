using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SpartanExtensions
{
    public static class DirectoryInfoExtensions
    {
        public static List<string> GetAllFilePathsWithinDirectoryAndSubDirectories(this DirectoryInfo directoryInfo)
        {
            var filePaths = new List<string>();

            directoryInfo.GetFiles()
                .Select(f => f.FullName)
                .ToList()
                .ForEach(filePaths.Add);

            var subdirectories = directoryInfo
                .GetDirectories()
                .ToList();

            subdirectories.ForEach(sd => filePaths.AddRange(sd.GetAllFilePathsWithinDirectoryAndSubDirectories()));

            return filePaths;
        }
    }
}
