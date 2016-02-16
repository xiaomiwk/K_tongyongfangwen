using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

namespace Utility.扩展
{
    public static class H字节处理
    {
        public static byte[] 合并(this byte[] 字节数组1, byte[] 字节数组2)
        {
            var result = new byte[字节数组1.Length + 字节数组2.Length];
            Buffer.BlockCopy(字节数组1, 0, result, 0, 字节数组1.Length);
            Buffer.BlockCopy(字节数组2, 0, result, 字节数组1.Length, 字节数组2.Length);
            return result;
        }

        public static byte[] 截取(this byte[] 源字节数组, int 起始位置, int 截取长度)
        {
            if (源字节数组 == null)
            {
                throw new ArgumentNullException("字节数组截取异常, 源字节数组不能为null");
            }
            if (源字节数组.Length < 起始位置 + 截取长度)
            {
                throw new ArgumentOutOfRangeException("字节数组截取异常, 源字节数组长度 < 起始位置 + 截取长度");
            }
            var result = new byte[截取长度];
            Buffer.BlockCopy(源字节数组, 起始位置, result, 0, 截取长度);
            return result;
        }

        public static byte[] 截取(this List<byte> 源字节数组, int 起始位置, int 截取长度)
        {
            var result = new byte[截取长度];
            for (int i = 0; i < 截取长度; i++)
            {
                result[i] = 源字节数组[起始位置 + i];
            }
            return result;

        }

        /// <returns>符合的位置, 如果没有符合的返回-1</returns>
        public static int 检索字节流(this List<byte> 源比特流, List<byte> 特征流, int 开始位置, int 检索位数)
        {
            for (int i = 开始位置; i < 开始位置 + 检索位数; i++)
            {
                //bool 检索成功 = true;
                //for (int j = 0; j < 特征流.Count; j++)
                //{
                //    if (特征流[j] != 源比特流[i + j])
                //    {
                //        检索成功 = false;
                //        break;
                //    }
                //}
                bool 检索成功 = !特征流.Where((t, j) => t != 源比特流[i + j]).Any();
                if (检索成功)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <returns>符合的位置, 如果没有符合的返回-1</returns>
        public static int 检索字节流(this List<byte> 源比特流, byte[] 特征流, int 开始位置, int 检索位数)
        {
            for (int i = 开始位置; i < 开始位置 + 检索位数; i++)
            {
                //bool 检索成功 = true;
                //for (int j = 0; j < 特征流.Length; j++)
                //{
                //    if (特征流[j] != 源比特流[i + j])
                //    {
                //        检索成功 = false;
                //        break;
                //    }
                //}
                bool 检索成功 = !特征流.Where((t, j) => t != 源比特流[i + j]).Any();
                if (检索成功)
                {
                    return i;
                }
            }
            return -1;
        }

        public static byte[] 反转字节序(this byte[] 源字节数组)
        {
            int len = 源字节数组.Length;
            var result = new byte[len];
            for (int i = 0; i < len; i++)
            {
                result[i] = 源字节数组[len - i - 1];
            }
            return result;
        }


        public static byte[] 输出内存(this IntPtr __地址, int __偏移量, int __长度)
        {
            var __结果 = new byte[__长度];
            for (int i = 0; i < __长度; i++)
            {
                __结果[i] = Marshal.ReadByte(__地址, __偏移量 + i);
            }
            return __结果;
        }

        public static string ToDebug(this byte[] __字节数组)
        {
            return BitConverter.ToString(__字节数组);
        }

        public static string ToUTF16(this byte[] __字节数组, bool __检验休止符 = true)
        {
            var __原始字符 = Encoding.Unicode.GetString(__字节数组);
            if (__检验休止符)
            {
                var __第一个结束符号位置 = __原始字符.IndexOf('\0');
                if (__第一个结束符号位置 >= 0)
                {
                    return __原始字符.Substring(0, __第一个结束符号位置);
                }
            }
            return __原始字符;
        }

        public static string ToASCII(this byte[] __字节数组)
        {
            var __原始字符 = Encoding.ASCII.GetString(__字节数组);
            var __第一个结束符号位置 = __原始字符.IndexOf('\0');
            if (__第一个结束符号位置 >= 0)
            {
                return __原始字符.Substring(0, __第一个结束符号位置);
            }
            return __原始字符;
        }

        public static byte[] ToASCII(this string 号码, int 长度)
        {
            var result = new byte[长度];
            var temp = Encoding.ASCII.GetBytes(号码);
            Buffer.BlockCopy(temp, 0, result, 0, Math.Min(temp.Length, 长度));
            return result;
        }

        public static byte[] ToUTF16(this string 号码, int 长度)
        {
            var result = new byte[长度];
            var temp = Encoding.Unicode.GetBytes(号码);
            Buffer.BlockCopy(temp, 0, result, 0, Math.Min(temp.Length, 长度));
            return result;
        }

        public static int ToInt(this IPAddress ip)
        {
            return BitConverter.ToInt32(ip.GetAddressBytes(), 0);
        }

    }
}
