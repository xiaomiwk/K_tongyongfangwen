using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace Utility.存储
{
    public static class HGzip压缩
    {
        public static void 压缩(string __源文件名, string __目标文件名 = null)
        {
            var __源文件 = new FileInfo(__源文件名);
            if (__目标文件名 == null)
            {
                __目标文件名 = __源文件.FullName + ".gz";
            }
            using (var __目的文件流 = File.Create(__目标文件名))
            {
                using (var __压缩流 = new GZipStream(__目的文件流, CompressionMode.Compress))
                {
                    using (var __源文件流 = __源文件.OpenRead())
                    {
                        __源文件流.CopyTo(__压缩流);
                        Debug.WriteLine("文件 {0} 从 {1} 压缩到 {2} bytes.", __源文件.Name, __源文件.Length, __目的文件流.Length);
                    }
                }
            }
        }

        public static void 解压(string __源文件名, string __目标文件名)
        {
            var __源文件 = new FileInfo(__源文件名);
            using (FileStream __源文件流 = __源文件.OpenRead())
            {
                using (FileStream __目标文件流 = File.Create(__目标文件名))
                {
                    using (var __解压流 = new GZipStream(__源文件流, CompressionMode.Decompress))
                    {
                        __解压流.CopyTo(__目标文件流);
                    }
                }
            }
        }

        public static byte[] 解压(string __源文件名)
        {
            var __源文件 = new FileInfo(__源文件名);
            using (FileStream __源文件流 = __源文件.OpenRead())
            {
                using (var __目标文件流 = new MemoryStream())
                {
                    using (var __解压流 = new GZipStream(__源文件流, CompressionMode.Decompress))
                    {
                        __解压流.CopyTo(__目标文件流);
                        var __字节流 = __目标文件流.ToArray();
                        return __字节流;
                    }
                }
            }
        }


        //static void 压缩(string startPath, string zipPath)
        //{
        //    ZipFile.CreateFromDirectory(startPath, zipPath);
        //}

        //static void 解压(string zipPath, string extractPath)
        //{
        //    ZipFile.ExtractToDirectory(zipPath, extractPath);
        //}

        //static void Main(string[] args)
        //{
        //    string startPath = @"c:\example\start";
        //    string zipPath = @"c:\example\result.zip";
        //    string extractPath = @"c:\example\extract";

        //    ZipFile.CreateFromDirectory(startPath, zipPath);

        //    ZipFile.ExtractToDirectory(zipPath, extractPath);
        //}

        //static void Main(string[] args)
        //{
        //    string zipPath = @"c:\example\start.zip";
        //    string extractPath = @"c:\example\extract";

        //    using (ZipArchive archive = ZipFile.OpenRead(zipPath))
        //    {
        //        foreach (ZipArchiveEntry entry in archive.Entries)
        //        {
        //            if (entry.FullName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
        //            {
        //                entry.ExtractToFile(Path.Combine(extractPath, entry.FullName));
        //            }
        //        }
        //    }
        //}

        //下一个示例使用 ZipArchive 类访问现有的 .zip 文件，并添加一个新文件为压缩文件。 新文件获取压缩何时将其添加到现有 .zip 文件。
        //static void Main(string[] args)
        //{
        //    using (FileStream zipToOpen = new FileStream(@"c:\users\exampleuser\release.zip", FileMode.Open))
        //    {
        //        using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
        //        {
        //            ZipArchiveEntry readmeEntry = archive.CreateEntry("Readme.txt");
        //            using (StreamWriter writer = new StreamWriter(readmeEntry.Open()))
        //            {
        //                writer.WriteLine("Information about this package.");
        //                writer.WriteLine("========================");
        //            }
        //        }
        //    }
        //}

    }
}
