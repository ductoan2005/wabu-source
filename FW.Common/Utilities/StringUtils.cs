using System;
using FW.Common.Objects;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Security;

namespace FW.Common.Utilities
{
    public static class StringUtils
    {
        public static string GenerateRandomPassWord() => Membership.GeneratePassword(8, 1);

        public static string GenerateQuerySort(List<SortOption> sortOptions)
        {
            if (sortOptions == null)
            {
                return string.Empty;
            }

            return string.Join(",", sortOptions.Where(p => !string.IsNullOrWhiteSpace(p.FieldName)).Select(p => p.FieldName + " " + p.SortType));
        }

        public static string GetRelativePath(string filespec, string folder)
        {
            Uri pathUri = new Uri(filespec);
            // Folders must end in a slash
            if (!folder.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                folder += Path.DirectorySeparatorChar;
            }
            Uri folderUri = new Uri(folder);
            return Uri.UnescapeDataString(folderUri.MakeRelativeUri(pathUri).ToString().Replace(Path.DirectorySeparatorChar, '/'));
        }

        public static string GetAbsolutePath(string path)
            => FileUtils.GetDomainAppPathPath() + path?.Replace('/', '\\');

    }
}
