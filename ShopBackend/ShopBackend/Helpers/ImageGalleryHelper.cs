using ShopBackend.App_Start;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ShopBackend.Helpers
{
    public class ImageGalleryHelper
    {
        public static string ROOT_DIR => Setting.Value["config_upload"];
        public const string FILTER = "(*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

        public static string GetRelativePathTo(FileSystemInfo from, FileSystemInfo to)
        {
            Func<FileSystemInfo, string> getPath = fsi =>
            {
                var d = fsi as DirectoryInfo;
                return d == null ? fsi.FullName : d.FullName.TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar;
            };

            var fromPath = getPath(from);
            var toPath = getPath(to);

            var fromUri = new Uri(fromPath);
            var toUri = new Uri(toPath);

            var relativeUri = fromUri.MakeRelativeUri(toUri);
            var relativePath = Uri.UnescapeDataString(relativeUri.ToString());

            return relativePath.Replace('/', Path.DirectorySeparatorChar);
        }


        /// <summary>
        ///  Duyệt tất cả các tệp trong một directory
        /// </summary>
        /// <param name="directory">relative directory path</param>
        /// <returns></returns>
        public static IEnumerable<string> BrowseFiles(string directory)
        {
            var full_path = ROOT_DIR + directory;
            var files = Directory.EnumerateFiles(full_path, "*.*", SearchOption.TopDirectoryOnly).ToList();

            files.RemoveAll(item =>
            {
                var jpq = item.EndsWith(".jpg");
                var jpeg = item.EndsWith(".jpeg");
                var png = item.EndsWith(".png");
                var bmp = item.EndsWith(".bmp");
                return !(jpq || jpeg || png || bmp);
            });

            for (int i = 0; i < files.Count; i++)
            {
                var from = new DirectoryInfo(ROOT_DIR);
                files[i] = GetRelativePathTo(from, new FileInfo(files[i]));
            }

            return files;
        }

        /// <summary>
        /// Duyệt tất cả các thư mục trong một directory
        /// </summary>
        /// <param name="directory">relative directory path</param>
        /// <returns></returns>
        public static IEnumerable<string> BrowseFolders(string directory)
        {
            var folders = Directory.EnumerateDirectories(ROOT_DIR + directory).ToList();
            for (int i = 0; i < folders.Count; i++)
            {
                var from = new DirectoryInfo(ROOT_DIR);
                folders[i] = "\\" + GetRelativePathTo(from, new FileInfo(folders[i]));
            }
            return folders;
        }

    }
}