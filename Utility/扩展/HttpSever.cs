using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web;
using Utility.通用;

namespace Utility.扩展
{
    public delegate byte[] 处理动态请求(string __页面, Dictionary<string, string> __参数);

    public class HttpSever
    {
        public int 端口 { get; set; }

        private HttpListener _监听器;

        private 处理动态请求 _处理动态请求;

        private string _动态请求后缀名;

        public Func<string, byte[]> 获取静态文件 { get; set; }

        public HttpSever(int __端口, string __动态请求后缀名, 处理动态请求 __处理方法)
        {
            端口 = __端口;
            _处理动态请求 = __处理方法;
            _动态请求后缀名 = __动态请求后缀名;
        }

        public void 使用磁盘文件(string __目录)
        {
            获取静态文件 = __文件名 =>
            {
                if (!File.Exists(Path.Combine(__目录, __文件名)))
                {
                    return new byte[0];
                }
                return File.ReadAllBytes(Path.Combine(__目录, __文件名));
            };
        }

        /// <summary>
        /// 将文件"属性"的"生成操作"设置为"嵌入的资源", 
        /// </summary>
        /// <param name="__程序集">例如:this.GetType().Assembly</param>
        /// <param name="__资源路径">例如:通用访问.WebUI</param>
        /// <returns></returns>
        public void 使用嵌入资源(Assembly __程序集, string __资源路径)
        {
            获取静态文件 = __文件名 =>
            {
                var __路径 = string.Format("{0}.{1}", __资源路径, __文件名.Replace('\\', '.'));
                var __stream = __程序集.GetManifestResourceStream(__路径);
                if (__stream == null)
                {
                    return new byte[0];
                }
                var __结果 = new byte[__stream.Length];
                __stream.Read(__结果, 0, __结果.Length);
                return __结果;
            };
        }

        public void 开启()
        {
            if (!HttpListener.IsSupported)
            {
                Debug.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                return;
            }
            if (_监听器 != null && _监听器.IsListening)
            {
                return;
            }

            _监听器 = new HttpListener();
            _监听器.Prefixes.Add(string.Format("http://localhost:{0}/", 端口));
            _监听器.Prefixes.Add(string.Format("http://127.0.0.1:{0}/", 端口));
            var __本机IP列表 = Dns.GetHostAddresses(Dns.GetHostName()).Where(q => q.AddressFamily == AddressFamily.InterNetwork).ToList();
            __本机IP列表.ForEach(q => _监听器.Prefixes.Add(string.Format("http://{1}:{0}/", 端口, q)));
            _监听器.Start();
            已开启 = true;
            H调试.记录提示("已开启");
            new Thread(() =>
            {
                try
                {
                    while (_监听器.IsListening)
                    {
                        _监听器.BeginGetContext(处理请求, _监听器).AsyncWaitHandle.WaitOne();
                    }
                }
                catch (Exception ex)
                {
                    if (已开启)
                    {
                        H调试.记录异常(ex);
                    }
                }
                已开启 = false;
            })
            { IsBackground = true }.Start();
        }

        public bool 已开启 { get; set; }

        void 处理请求(IAsyncResult __凭据)
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
                var __参数 = 获取COOKIES数据(__请求).Concat(获取GET数据(__请求)).Concat(获取POST数据(__请求)).ToDictionary(q => q.Key, q => q.Value);
                var __响应内容 = 处理Web接收(__上下文, __页面, __参数);
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

        Dictionary<string, string> 获取GET数据(HttpListenerRequest __请求)
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

        Dictionary<string, string> 获取POST数据(HttpListenerRequest __请求)
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

        Dictionary<string, string> 获取COOKIES数据(HttpListenerRequest __请求)
        {
            var __cookie参数字典 = new Dictionary<string, string>();
            var __cookie数据 = __请求.Cookies;
            for (int i = 0; i < __cookie数据.Count; i++)
            {
                __cookie参数字典[__cookie数据[i].Name] = __cookie数据[i].Value;
            }
            return __cookie参数字典;
        }

        public void 关闭()
        {
            H调试.记录提示("关闭");
            if (_监听器 != null)
            {
                _监听器.Close();
            }
            已开启 = false;
        }

        byte[] 处理Web接收(HttpListenerContext __上下文, string __页面, Dictionary<string, string> __参数)
        {
            if (__页面 == "/")
            {
                __页面 = "/index.html";
            }
            var __文件名 = __页面.Replace('/', '\\').Remove(0, 1);
            var __最后点位置 = __文件名.LastIndexOf('.');
            var __后缀名 = __文件名.Substring(__最后点位置 + 1);
            if (__最后点位置 < 0 || __后缀名 == _动态请求后缀名)
            {
                __上下文.Response.ContentType = "application/json;charset=utf-8";
                return _处理动态请求(__页面, __参数);
            }
            if (获取静态文件 == null)
            {
                return new byte[0];
            }
            switch (__后缀名)
            {
                case "png":
                    __上下文.Response.ContentType = "image/png";
                    break;
                case "gif":
                    __上下文.Response.ContentType = "image/gif";
                    break;
                case "cur":
                    __上下文.Response.ContentType = "application/octet-stream";
                    break;
                case "html":
                    __上下文.Response.ContentType = "text/html;charset=utf-8";
                    break;
                case "css":
                    __上下文.Response.ContentType = "text/css;charset=utf-8";
                    break;
                case "js":
                    __上下文.Response.ContentType = "application/javascript;charset=utf-8";
                    break;
                default:
                    return new byte[0];
            }
            return 获取静态文件(__文件名);
        }
    }

    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        var __服务器 = new HttpSever(9999, "j", new H处理请求().处理);
    //        var __目录 = @"E:\项目--中心网管客户端\中心网管客户端";
    //        //var __目录 = Path.Combine(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location), "WebUI");
    //        //var __目录 = @"C:\Program Files\Apache Software Foundation\Tomcat 8.0\webapps\ROOT";
    //        __服务器.使用磁盘文件(__目录);
    //        __服务器.开启();
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


