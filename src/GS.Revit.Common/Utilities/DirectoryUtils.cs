using System;
using System.IO;
using System.Security.AccessControl;

namespace GS.Revit.Common.Utilities
{
    public static class DirectoryUtils
    {
        // NOTE This returns true on Revit Samples folder, but we still can't write there.
        /// <summary>
        /// Checks if user can write to the directory at the <see cref="path"/>.
        /// </summary>
        /// <param name="path">Path to the directory to check.</param>
        /// <returns><see cref="true"/> if user can write there, <see cref="false"/> otherwise.</returns>
        public static bool HasWritePermission(string path)
        {
            var writeAllow = false;
            var writeDeny = false;

            while (!Directory.Exists(path))
            {
                var index = path.LastIndexOf(Path.DirectorySeparatorChar);
                if (index == -1)
                {
                    // Impossible path.
                    return false;
                }

                path = path.Substring(0, index);
            }

            var accessControlList = Directory.GetAccessControl(path);
            if (accessControlList is null)
                return false;

            var accessRules = accessControlList
                .GetAccessRules(true, true, typeof(System.Security.Principal.SecurityIdentifier));
            if (accessRules is null)
            {
                return false;
            }

            foreach (FileSystemAccessRule rule in accessRules)
            {
                if ((FileSystemRights.Write & rule.FileSystemRights) != FileSystemRights.Write)
                {
                    continue;
                }

                if (rule.AccessControlType == AccessControlType.Allow)
                {
                    writeAllow = true;
                }
                else if (rule.AccessControlType == AccessControlType.Deny)
                {
                    writeDeny = true;
                }
            }

            return writeAllow && !writeDeny;
        }

        /// <summary>
        /// Checks folder for write permission.
        /// Returns the folder or path to the Desktop if there is no such permission.
        /// </summary>
        public static bool CanWrite(string path, out string pathOrDefault)
        {
            pathOrDefault = path;
            if (Directory.Exists(path) && HasWritePermission(path) && CanWrite(path))
            {
                return true;
            }

            pathOrDefault = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            return false;
        }

        /// <summary>
        /// Checks if user can write to the directory at the <see cref="path"/>.
        /// </summary>
        /// <param name="path">Path to the directory to check.</param>
        /// <returns><see cref="true"/> if user can write there, <see cref="false"/> otherwise.</returns>
        private static bool CanWrite(string path)
        {
            try
            {
                var dir = Path.HasExtension(path) ? Path.GetDirectoryName(path) : path;
                var testDirPath = Path.Combine(dir, "Access Test Directory");
                Directory.CreateDirectory(testDirPath);

                // Can write.
                Directory.Delete(testDirPath);
                return true;
            }
            catch
            {
                // Can't create files there.
                return false;
            }
        }
    }
}
