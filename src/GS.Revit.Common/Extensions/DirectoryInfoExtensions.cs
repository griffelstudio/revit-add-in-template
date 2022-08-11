using System.IO;

namespace GS.Revit.Common.Extensions
{
    public static class DirectoryInfoExtensions
    {
        /// <summary>
        /// Copy all files and subdirectories to another directory.
        /// </summary>
        public static void CopyAllTo(this DirectoryInfo source, DirectoryInfo target)
        {
            // Check source and target are not the same.
            if (source.FullName.ToLower() == target.FullName.ToLower())
            {
                return;
            }

            // Check if target directory exists and create it if it doesn't.
            if (Directory.Exists(target.FullName) == false)
            {
                Directory.CreateDirectory(target.FullName);
            }

            // Copy all files to target folder.
            foreach (var file in source.GetFiles())
            {
                file.CopyTo(Path.Combine(target.ToString(), file.Name), true);
            }

            // Use recursion for subdirectories.
            foreach (var sourceSubDir in source.GetDirectories())
            {
                var targetSubDir = target.CreateSubdirectory(sourceSubDir.Name);
                sourceSubDir.CopyAllTo(targetSubDir);
            }
        }
    }
}
