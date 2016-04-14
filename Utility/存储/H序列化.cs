using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.IsolatedStorage;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace Utility.存储
{
    public class H序列化
    {
        //public static void SaveData(string data, string fileName)
        //{
        //    using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
        //    {
        //        using (var isfs = new IsolatedStorageFileStream(fileName, FileMode.Create, isf))
        //        {
        //            using (var sw = new StreamWriter(isfs))
        //            {
        //                sw.Write(data); sw.Close();
        //            }
        //        }
        //    }
        //}

        //public static string LoadData(string fileName)
        //{
        //    string data = String.Empty;
        //    using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
        //    {
        //        using (var isfs = new IsolatedStorageFileStream(fileName, FileMode.Open, isf))
        //        {
        //            using (var sr = new StreamReader(isfs))
        //            {
        //                string lineOfData;
        //                while ((lineOfData = sr.ReadLine()) != null) data += lineOfData;
        //            }
        //        }
        //    }
        //    return data;
        //}

        public static void 二进制存储(object 可序列化对象, string 文件路径)
        {
            using (var __文件流 = H路径.创建文件(文件路径))
            {
                var __格式化器 = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.Clone));
                __格式化器.Serialize(__文件流, 可序列化对象);
            }
        }

        public static object 二进制读取(string 文件路径)
        {
            var __文件流 = H路径.打开文件(文件路径);
            if (__文件流 != null)
            {
                using (__文件流)
                {
                    var __格式化器 = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.Clone));
                    try
                    {
                        var __对象 = __格式化器.Deserialize(__文件流);
                        return __对象;
                    }
                    catch (Exception ex)
                    {
                        通用.H调试.记录异常(ex, "二进制读取失败", 文件路径);
                        return null;
                    }
                }
            }
            return null;
        }

        public static void XML存储<T>(T 可序列化对象, string 文件路径)
        {
            var __格式化器 = new XmlSerializer(typeof(T));
            using (var __文件流 = H路径.创建文件(文件路径))
            {
                using (var __写流 = new StreamWriter(__文件流))
                {
                    __格式化器.Serialize(__写流, 可序列化对象);
                }
            }
        }

        public static T XML读取<T>(string 文件路径)
        {
            var __文件流 = H路径.打开文件(文件路径);
            if (__文件流 != null)
            {
                using (__文件流)
                {
                    var __格式化器 = new XmlSerializer(typeof(T));
                    return (T)__格式化器.Deserialize(__文件流);
                }
            }
            return default(T);
        }

        public static string ToXML字符串<T>(T 可序列化对象)
        {
            var __字符串 = new StringBuilder();
            var __XML书写器 = XmlWriter.Create(__字符串);
            var __序列化 = new XmlSerializer(typeof(T));
            __序列化.Serialize(__XML书写器, 可序列化对象);
            return __字符串.ToString();
        }

        public static T FromXML字符串<T>(string 字符串)
        {
            var __格式化器 = new XmlSerializer(typeof(T));
            return (T)__格式化器.Deserialize(XmlReader.Create(字符串));
        }

        public static string ToJSON字符串<T>(T 可序列化对象, bool 标识类型 = false)
        {
            if (标识类型)
            {
                return new JavaScriptSerializer(new SimpleTypeResolver()).Serialize(可序列化对象);
            }
            return new JavaScriptSerializer().Serialize(可序列化对象);
        }

        public static T FromJSON字符串<T>(string 字符串, bool 标识类型 = false, int __最大长度 = 5000000)
        {
            if (标识类型)
            {
                return new JavaScriptSerializer(new SimpleTypeResolver()).Deserialize<T>(字符串);
            }

            return new JavaScriptSerializer() { MaxJsonLength = __最大长度 }.Deserialize<T>(字符串);
        }

        /// <returns>x,x-x,x</returns>
        public static string 单值列表转字符串(List<int> __列表)
        {
            var __段列表 = 单值列表转段列表(__列表);
            var __描述 = new StringBuilder();
            __段列表.ForEach(q =>
            {
                if (q.Item1 == q.Item2)
                {
                    __描述.Append(q.Item1).Append(',');
                }
                else
                {
                    __描述.AppendFormat("{0}-{1},", q.Item1, q.Item2);
                }
            });
            if (__描述.Length > 0)
            {
                __描述.Remove(__描述.Length - 1, 1);
            }
            return __描述.ToString();
        }

        /// <param name="__字符串">x,x-x,x</param>
        public static List<int> 字符串转单值列表(string __字符串)
        {
            if (string.IsNullOrEmpty(__字符串))
            {
                return new List<int>();
            }
            __字符串 = __字符串.Replace(" ", "");
            var __结果 = new List<int>();
            var __列表 = __字符串.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < __列表.Length; i++)
            {
                var __分段 = __列表[i].Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                if (__分段.Length == 1)
                {
                    __结果.Add(int.Parse(__分段[0]));
                    continue;
                }
                var __起始 = int.Parse(__分段[0]);
                var __终止 = int.Parse(__分段[1]);
                for (int j = __起始; j <= __终止; j++)
                {
                    __结果.Add(j);
                }
            }
            return __结果;
        }

        public static List<Tuple<int, int>> 单值列表转段列表(List<int> __号码列表)
        {
            if (__号码列表.Count == 0)
            {
                return new List<Tuple<int, int>>();
            }
            var __结果 = new List<Tuple<int, int>>();
            __号码列表.Sort();
            var __起始号码 = __号码列表[0];
            var __起始索引 = 0;
            for (int i = 1; i < __号码列表.Count; i++)
            {
                if (__号码列表[i] > __起始号码 + i - __起始索引)
                {
                    __结果.Add(new Tuple<int, int>(__起始号码, __号码列表[i - 1]));
                    __起始号码 = __号码列表[i];
                    __起始索引 = i;
                }
            }
            __结果.Add(new Tuple<int, int>(__起始号码, __号码列表[__号码列表.Count - 1]));
            return __结果;
        }

        public static string[,] 分割表格(string __制表符表格)
        {
            if (string.IsNullOrEmpty(__制表符表格) || !__制表符表格.Contains("\r\n"))
            {
                return null;
            }
            var __行数 = __制表符表格.Count(q => q == '\r');
            var __列数 = __制表符表格.Count(q => q == '\t') / __行数;
            var __结果 = new string[__行数, __列数];
            var __所有行 = __制表符表格.Split(new[] { "\r\n" }, StringSplitOptions.None);
            for (int i = 0; i < __所有行.Length - 1; i++)
            {
                if (string.IsNullOrEmpty(__所有行[i]))
                {
                    continue;
                }
                var __所有列 = __所有行[i].Split(new char[] { '\t' }, StringSplitOptions.None);
                for (int j = 0; j < __所有列.Length - 1; j++)
                {
                    __结果[i, j] = __所有列[j];
                }
            }
            return __结果;
        }

        public static string 合成表格(string[,] __表格)
        {
            var __结果 = new StringBuilder();
            for (int i = 0; i < __表格.GetLength(0); i++)
            {
                for (int j = 0; j < __表格.GetLength(1); j++)
                {
                    __结果.AppendFormat("{0}\t", __表格[i, j]);
                }
                __结果.Append("\r\n");
            }
            return __结果.ToString();
        }

        public static string AES解压(string __返回值)
        {
            using (var __目标流 = new MemoryStream())
            {
                using (var __源流 = new MemoryStream(Convert.FromBase64String(__返回值)))
                {
                    using (var __解压器 = new GZipStream(__源流, CompressionMode.Decompress))
                    {
                        __解压器.CopyTo(__目标流);
                    }
                }
                var __解压 = Encoding.UTF8.GetString(__目标流.GetBuffer());
                return __解压;
            }
        }

        public static string AES压缩(string __字符串)
        {
            using (var __目标流 = new MemoryStream())
            {
                using (var __源流 = new MemoryStream(Encoding.UTF8.GetBytes(__字符串)))
                {
                    using (var __压缩器 = new GZipStream(__目标流, CompressionMode.Compress))
                    {
                        __源流.CopyTo(__压缩器);
                    }
                }
                var __压缩 = Convert.ToBase64String(__目标流.GetBuffer());
                Debug.WriteLine("压缩率 {2}%, {0} > {1}", __字符串.Length, __压缩.Length, __压缩.Length * 100 / __字符串.Length);
                return __压缩;
            }
        }

    }
}
