using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Utility.通用;

namespace Utility.扩展
{
    public delegate byte[] 处理动态请求(string __页面, Dictionary<string, string> __get参数, Dictionary<string, string> __cookie参数, Dictionary<string, string> __post参数);

    public class HttpSever
    {
        public int 端口 { get; set; }

        private HttpListener _监听器;

        private 处理动态请求 _处理方法;

        private string _动态请求后缀名;

        private string _目录;

        public void 开启(int __端口, string __动态请求后缀名, 处理动态请求 __处理方法, string __静态文件目录)
        {
            if (!HttpListener.IsSupported)
            {
                Debug.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                return;
            }
            if (_监听器.IsListening)
            {
                return;
            }
            端口 = __端口;
            _处理方法 = __处理方法;
            _动态请求后缀名 = __动态请求后缀名;
            _目录 = __静态文件目录;

            _监听器 = new HttpListener();
            _监听器.Prefixes.Add(string.Format("HTTP://localhost:{0}/", 端口));
            _监听器.Prefixes.Add(string.Format("HTTP://127.0.0.1:{0}/", 端口));
            var __本机IP列表 = Dns.GetHostAddresses(Dns.GetHostName()).Where(q => q.AddressFamily == AddressFamily.InterNetwork).ToList();
            __本机IP列表.ForEach(q => _监听器.Prefixes.Add(string.Format("HTTP://{1}:{0}/", 端口, q)));
            _监听器.Start();
            已开启 = true;

            Task.Factory.StartNew(() =>
            {
                while (_监听器.IsListening)
                {
                    IAsyncResult __凭据 = _监听器.BeginGetContext(处理请求, _监听器);
                    __凭据.AsyncWaitHandle.WaitOne();
                }
                H调试.记录提示("关闭");
                已开启 = false;
            });
            H调试.记录提示("开启");
        }

        public bool 已开启 { get; set; }

        public void 处理请求(IAsyncResult __凭据)
        {
            HttpListenerResponse __响应 = null;
            try
            {
                var __监听器 = (HttpListener)__凭据.AsyncState;
                var __上下文 = __监听器.EndGetContext(__凭据);
                __响应 = __上下文.Response;

                var __请求 = __上下文.Request;
                var __页面 = HttpUtility.UrlDecode(__请求.RawUrl);
                if (__页面.IndexOf('?') > 0)
                {
                    __页面 = __页面.Substring(0, __页面.IndexOf('?'));
                }
                var __get参数字典 = 获取GET数据(__请求);
                var __post参数字典 = 获取POST数据(__请求);
                var __cookie数据 = __请求.Cookies;
                var __cookie参数字典 = new Dictionary<string, string>();
                for (int i = 0; i < __cookie数据.Count; i++)
                {
                    __cookie参数字典[__cookie数据[i].Name] = __cookie数据[i].Value;
                }
                var __响应内容 = 处理Web接收(__上下文, __页面, __get参数字典, __cookie参数字典, __post参数字典);
                __响应.ContentLength64 = __响应内容.Length;
                __响应.OutputStream.Write(__响应内容, 0, __响应内容.Length);
                __响应.OutputStream.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                if (__响应 != null)
                {
                    __响应.Close();
                }
            }
        }

        public static Dictionary<string, string> 获取GET数据(HttpListenerRequest __请求)
        {
            var __内容 = HttpUtility.UrlDecode(__请求.Url.Query);
            if (string.IsNullOrEmpty(__内容.Trim()))
            {
                return new Dictionary<string, string>();
            }
            __内容 = __内容.Substring(1);
            var __字典 = new Dictionary<string, string>();
            var __分割 = __内容.Split('&');
            for (int i = 0; i < __分割.Length; i++)
            {
                var __temp = __分割[i];
                var __位置 = __temp.IndexOf("=");
                if (__位置 > 0)
                {
                    var __key = __temp.Substring(0, __位置).Trim();
                    if (__位置 < __temp.Length)
                    {
                        var __value = __temp.Substring(__位置 + 1).Trim();
                        __字典[__key] = __value;
                    }
                }
            }
            return __字典;
        }

        public static Dictionary<string, string> 获取POST数据(HttpListenerRequest __请求)
        {
            if (!__请求.HasEntityBody)
            {
                return new Dictionary<string, string>();
            }
            var __数据流 = __请求.InputStream;
            var __编码 = __请求.ContentEncoding;
            var __阅读器 = new StreamReader(__数据流, __编码);
            var __内容 = HttpUtility.UrlDecode(__阅读器.ReadToEnd());
            //Debug.WriteLine(__内容);
            __数据流.Close();
            __阅读器.Close();
            var __字典 = new Dictionary<string, string>();
            var __分割 = __内容.Split('&');
            for (int i = 0; i < __分割.Length; i++)
            {
                var __temp = __分割[i];
                var __位置 = __temp.IndexOf("=");
                if (__位置 > 0)
                {
                    var __key = __temp.Substring(0, __位置).Trim();
                    if (__位置 < __temp.Length)
                    {
                        var __value = __temp.Substring(__位置 + 1).Trim();
                        __字典[__key] = __value;
                    }
                }
            }
            return __字典;
        }

        public void 关闭()
        {
            H调试.记录提示("关闭");
            已开启 = false;
            _监听器.Close();
        }

        public byte[] 处理Web接收(HttpListenerContext __上下文, string __页面, Dictionary<string, string> __get参数, Dictionary<string, string> __cookie参数, Dictionary<string, string> __post参数)
        {
            //Debug.WriteLine(string.Format("页面: {0}", HttpUtility.UrlDecode(__页面)));
            //if (__get参数 != null && __get参数.Count > 0)
            //{
            //    Debug.WriteLine("get参数");
            //    foreach (var __kv in __get参数)
            //    {
            //        Debug.WriteLine("   {0,-10}: {1}", __kv.Key, __kv.Value);
            //    }
            //}
            //if (__post参数 != null && __post参数.Count > 0)
            //{
            //    Debug.WriteLine("post参数");
            //    foreach (var __kv in __post参数)
            //    {
            //        Debug.WriteLine("   {0,-10}: {1}", __kv.Key, __kv.Value);
            //    }
            //}
            //if (__cookie参数 != null && __cookie参数.Count > 0)
            //{
            //    Debug.WriteLine("cookie参数");
            //    foreach (var __kv in __cookie参数)
            //    {
            //        Debug.WriteLine("   {0,-10}: {1}", __kv.Key, __kv.Value);
            //    }
            //}

            var __文件名 = "";
            if (__页面 == "/")
            {
                __页面 = "/index.html";
            }
            __文件名 = __页面.Replace('/', '\\').Remove(0, 1);
            var __最后点位置 = __文件名.LastIndexOf('.');
            if (__最后点位置 < 0)
            {
                if (string.IsNullOrEmpty(_动态请求后缀名))
                {
                    return _处理方法(__页面, __get参数, __cookie参数, __post参数);
                }
                return new byte[0];
            }
            var __后缀名 = __文件名.Substring(__最后点位置 + 1);
            if (__后缀名 == _动态请求后缀名) {
                __上下文.Response.ContentType = "application/json;charset=utf-8";
                return _处理方法(__页面, __get参数, __cookie参数, __post参数);
            }
            switch (__后缀名)
            {
                case "png":
                    __上下文.Response.ContentType = "image/png";
                    if (!File.Exists(Path.Combine(_目录, __文件名)))
                    {
                        return new byte[0];
                    }
                    return File.ReadAllBytes(Path.Combine(_目录, __文件名));
                case "gif":
                    __上下文.Response.ContentType = "image/gif";
                    if (!File.Exists(Path.Combine(_目录, __文件名)))
                    {
                        return new byte[0];
                    }
                    return File.ReadAllBytes(Path.Combine(_目录, __文件名));
                case "cur":
                    __上下文.Response.ContentType = "application/octet-stream";
                    if (!File.Exists(Path.Combine(_目录, __文件名)))
                    {
                        return new byte[0];
                    }
                    return File.ReadAllBytes(Path.Combine(_目录, __文件名));
                case "html":
                    //__上下文.Response.Redirect("index.html");
                    //break;
                    __上下文.Response.ContentType = "text/html";
                    return File.ReadAllBytes(Path.Combine(_目录, __文件名));
               case "css":
                    if (!File.Exists(Path.Combine(_目录, __文件名)))
                    {
                        return 文本编码("<HTML><BODY> 无效地址 </BODY></HTML>");
                    }
                    __上下文.Response.ContentType = "text/css;charset=utf-8";
                    break;
                case "js":
                    if (!File.Exists(Path.Combine(_目录, __文件名)))
                    {
                        return 文本编码("<HTML><BODY> 无效地址 </BODY></HTML>");
                    }
                    __上下文.Response.ContentType = "application/javascript;charset=utf-8";
                    break;
                default:
                    return new byte[0];
            }
            var __文件路径 = Path.Combine(_目录, __文件名);
            if (!File.Exists(__文件路径))
            {
                return new byte[0];
            }
            return 文本编码(File.ReadAllText(__文件路径));
        }

        public byte[] 文本编码(string __内容)
        {
            return Encoding.UTF8.GetBytes(__内容);
        }
    }

    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        //var __目录 = Path.Combine(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location), "WebUI");
    //        //var __目录 = @"C:\Program Files\Apache Software Foundation\Tomcat 8.0\webapps\ROOT";
    //        var __目录 = @"E:\项目--中心网管客户端\中心网管客户端";
    //        var __服务器 = new HttpSever();
    //        __服务器.开启(9999, "j", new H处理请求().处理, __目录);
    //        Console.WriteLine("OK");
    //        Console.ReadLine();
    //        __服务器.关闭();
    //    }
    //}

    //switch (__文件名)
    //{
    //    case "市级网管.j":
    //        var __查询通知 = HJSON.反序列化<M查询通知>(__请求参数);
    //        var __起始Id = __查询通知.起始Id;
    //        var __条数 = __查询通知.条数;
    //        __发送 = HJSON.序列化(_所有通知.Where(q => q.Id > __起始Id).Take(__条数));
    //        H日志输出.记录(string.Format("发送 {0}", __发送));
    //        return 文本编码(__发送);
    //    default:
    //        return new byte[0];
    //}

}
