using FW.Common.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System.IO;
using System.Linq;
using System.Web;

namespace FW.Common.Utilities
{
    public static class FileUtils
    {
        public static string GetDomainAppPathPath() => HttpRuntime.AppDomainAppPath;

        public static string GetServerStoragePath() => HttpRuntime.AppDomainAppPath + CommonSettings.GetServerStoragePath;

        public static DirectoryInfo CreateDirectory(string path) => Directory.Exists(path) ? null : Directory.CreateDirectory(path);

        public static void DeleteFileInDirectory(string path)
        {
            var di = new DirectoryInfo(path);
            if (di.Exists)
            {
                foreach (var file in di.EnumerateFiles())
                {
                    file.Delete();
                }
            }
        }

        public static void DeleteDirectory(string path) => Directory.Delete(path, true);

        public static void DeleteFileIfExists(string path)
        {
            var file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
            }
        }

        public static string SaveFileToServer(HttpPostedFileBase file, string path)
        {
            CreateDirectory(path);
            var fileName = RenameFileIfExists(Path.Combine(path), file.FileName);
            var filePath = Path.Combine(path, fileName);
            file.SaveAs(filePath);
            return filePath;
        }

        public static string RenameFileIfExists(string path, string fileName)
        {
            var result = fileName;
            var di = new DirectoryInfo(path);
            if (di.Exists)
            {
                if (di.EnumerateFiles().Any(x => fileName.Equals(x.Name)))
                {
                    var pathFileName = Path.Combine(path, fileName);
                    var fileNameNoExtension = Path.GetFileNameWithoutExtension(pathFileName);
                    var fileExtension = Path.GetExtension(pathFileName);
                    var count = 1;
                    while (true)
                    {
                        var renameFile = fileNameNoExtension + $" ({count})" + fileExtension;
                        //var pathRenameFile = Path.Combine(path, renameFile);
                        if (di.EnumerateFiles().Any(x => renameFile.Equals(x.Name)))
                        {
                            count++;
                        }
                        else
                        {
                            result = renameFile;
                            return result;
                        }
                    }
                }
            }

            return result;
        }

        public static FileStream ReadFileStreamFromPath(string path) => File.Exists(path) ? File.OpenRead(path) : null;

        public static void MoveDirectory(string directoryPath, string destinationPath)
        {
            var di = new DirectoryInfo(directoryPath);
            CreateDirectory(destinationPath);
            if (di.Exists)
            {
                foreach (var file in di.EnumerateFiles())
                {
                    file.MoveTo(Path.Combine(destinationPath, file.Name));
                }
            }
        }

        public static IFormFile ConvertToIFormFile(HttpPostedFileBase file)
        {
            var byteArray = ConvertToBytes(file);
            var formFile = new FormFile(
                new MemoryStream(byteArray),
                0,
                byteArray.Length,
                file.FileName,
                file.FileName
            )
            {
                Headers = new HeaderDictionary(),
                ContentType = file.ContentType
            };

            return formFile;
        }

        private static byte[] ConvertToBytes(HttpPostedFileBase file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.InputStream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
