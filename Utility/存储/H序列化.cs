using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.IsolatedStorage;
using System.IO;
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
            var __格式化器 = new XmlSerializer(typeof (T));
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
            if(__文件流 != null)
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

        public static T FromJSON字符串<T>(string 字符串, bool 标识类型 = false)
        {
            if (标识类型)
            {
                return new JavaScriptSerializer(new SimpleTypeResolver()).Deserialize<T>(字符串);
            }
            return new JavaScriptSerializer().Deserialize<T>(字符串);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="__列表"></param>
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

        public static List<Tuple<int, int>> 单值列表转段列表(List<int> __号码列表)
        {
            if (__号码列表.Count == 0)
            {
                return new List<Tuple<int, int>>();
            }
            var __结果 = new List<Tuple<int, int>>();
            __号码列表.Sort();
            var __起始号码 = __号码列表[0];
            for (int i = 0; i < __号码列表.Count; i++)
            {
                if (__号码列表[i] > __起始号码 + i)
                {
                    __结果.Add(new Tuple<int, int>(__起始号码, __号码列表[i - 1]));
                    __起始号码 = __号码列表[i];
                }
            }
            __结果.Add(new Tuple<int, int>(__起始号码, __号码列表[__号码列表.Count - 1]));
            return __结果;
        }

    }
}
