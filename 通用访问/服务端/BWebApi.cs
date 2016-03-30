using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Web;
using INET;
using 通用访问.DTO;

namespace 通用访问.服务端
{
    class BWebApi
    {
        private Func<Dictionary<string, Func<M对象>>> _所有对象;

        public int 端口 { get; private set; }

        private string _密码 = "k";

        private HttpListener _监听器;

        public BWebApi(int __端口, Func<Dictionary<string, Func<M对象>>> __获取对象)
        {
            端口 = __端口;
            _所有对象 = __获取对象;
        }

        public void 开启()
        {
            if (!HttpListener.IsSupported)
            {
                Debug.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                return;
            }
            if (已开启)
            {
                Debug.WriteLine("BT服务端 已开启");
                return;
            }

            _监听器 = new HttpListener();
            _监听器.Prefixes.Add(string.Format("HTTP://localhost:{0}/", 端口));
            _监听器.Prefixes.Add(string.Format("HTTP://127.0.0.1:{0}/", 端口));
            var __本机IP列表 = Dns.GetHostAddresses(Dns.GetHostName()).Where(q => q.AddressFamily == AddressFamily.InterNetwork).ToList();
            __本机IP列表.ForEach(q => _监听器.Prefixes.Add(string.Format("HTTP://{1}:{0}/", 端口, q)));
            _监听器.Start();
            已开启 = true;

            new Thread(() =>
            {
                while (_监听器.IsListening)
                {
                    IAsyncResult __凭据 = _监听器.BeginGetContext(处理请求, _监听器);
                    //Debug.WriteLine("Waiting for request to be processed asyncronously.");
                    __凭据.AsyncWaitHandle.WaitOne();
                    //Debug.WriteLine("Request processed asyncronously.");
                }
                Debug.WriteLine("BWebApi 已关闭");
                已开启 = false;
            }) { IsBackground = true }.Start();
        }

        public bool 已开启 { get; private set; }

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
                H日志输出.记录(ex);
            }
            finally
            {
                if (__响应 != null)
                {
                    __响应.Close();
                }
            }
        }

        static Dictionary<string, string> 获取GET数据(HttpListenerRequest __请求)
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

        static Dictionary<string, string> 获取POST数据(HttpListenerRequest __请求)
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
            已开启 = false;
            if (_监听器 != null)
            {
                _监听器.Close();
            }
        }

        byte[] 处理Web接收(HttpListenerContext __上下文, string __页面, Dictionary<string, string> __get参数, Dictionary<string, string> __cookie参数, Dictionary<string, string> __post参数)
        {
            var __文件名 = "";
            if (__页面 == "/")
            {
                __页面 = "/index.html";
            }
            __文件名 = __页面.Replace('/', '\\').Remove(0, 1);
            var __最后点位置 = __文件名.LastIndexOf('.');
            if (__最后点位置 < 0)
            {
                return new byte[0];
            }
            var __后缀名 = __文件名.Substring(__最后点位置 + 1);
            switch (__后缀名)
            {
                case "png":
                    __上下文.Response.ContentType = "image/png";
                    break;
                case "html":
                    if (__文件名 != "index.html" && (__上下文.Request.Cookies["token"] == null || __上下文.Request.Cookies["token"].Value != _密码))
                    {
                        __上下文.Response.Redirect("index.html");
                        return new byte[0];
                    }
                    __上下文.Response.ContentType = "text/html;charset=utf-8";
                    break;
                case "k":
                    Debug.WriteLine(string.Format("页面: {0}", HttpUtility.UrlDecode(__页面)));
                    __上下文.Response.ContentType = "application/json;charset=utf-8";
                    var __发送 = "";
                    switch (__文件名)
                    {
                        case "login.k":
                            var __密码 = __post参数["password"];
                            if (__密码 == _密码)
                            {
                                var __令牌 = "k";
                                __上下文.Response.Cookies.Add(new Cookie("token", __令牌) { Expires = DateTime.Now.AddDays(1) });
                                __发送 = "{ \"成功\":true}";
                                H日志输出.记录(string.Format("发送 {0}", __发送));
                                return 文本编码(__发送);
                            }
                            __发送 = "{ \"成功\":false, 描述:'密码错误'}";
                            H日志输出.记录(string.Format("发送 {0}", __发送));
                            return 文本编码(__发送);
                        case "heart.k":
                            __发送 = HJSON.序列化(DateTime.Now.ToString());
                            return 文本编码(__发送);
                        case "objects.k":
                            __发送 = HJSON.序列化(_所有对象().Values.ToList().Select(q => q().概要).ToList().OrderBy(q => q.分类));
                            H日志输出.记录(string.Format("发送 {0}", __发送));
                            return 文本编码(__发送);
                        case "object.k":
                            var __对象名称 = __get参数["对象名称"];
                            if (_所有对象().ContainsKey(__对象名称))
                            {
                                var __对象 = _所有对象()[__对象名称]();
                                __发送 = HJSON.序列化(__对象.明细);
                                H日志输出.记录(string.Format("发送 {0}", __发送));
                                return 文本编码(__发送);
                            }
                            return new byte[0];
                        case "method.k":
                            __对象名称 = __get参数["对象名称"];
                            var __方法名称 = __get参数["方法名称"];
                            var __实参列表 = HJSON.反序列化<List<M实参>>(__get参数["实参列表"]);
                            if (_所有对象().ContainsKey(__对象名称))
                            {
                                var __对象 = _所有对象()[__对象名称]();
                                var __方法 = __对象.明细.方法列表.Find(q => q.名称 == __方法名称);
                                if (__方法 != null)
                                {
                                    var __执行成功 = true;
                                    var __执行描述 = "";
                                    var __返回值 = "";
                                    try
                                    {
                                        __返回值 = __对象.执行方法(__方法名称, M实参.列表转字典(__实参列表), null);
                                    }
                                    catch (Exception ex)
                                    {
                                        __执行描述 = ex.Message;
                                        __执行成功 = false;
                                    }
                                    __发送 = HJSON.序列化(new M方法执行结果() { 成功 = __执行成功, 描述 = __执行描述, 返回值 = __返回值 });
                                    H日志输出.记录(string.Format("发送 {0}", __发送));
                                    return 文本编码(__发送);
                                }
                            }
                            return new byte[0];
                        case "property.k":
                            __对象名称 = __get参数["对象名称"];
                            var __属性名称 = __get参数["属性名称"];
                            if (_所有对象().ContainsKey(__对象名称))
                            {
                                var __对象 = _所有对象()[__对象名称]();
                                var __属性 = __对象.明细.属性列表.Find(q => q.名称 == __属性名称);
                                if (__属性 != null)
                                {
                                    var __执行成功 = true;
                                    var __执行描述 = "";
                                    var __返回值 = "";
                                    try
                                    {
                                        __返回值 = __对象.计算属性(__属性名称);
                                    }
                                    catch (Exception ex)
                                    {
                                        __执行描述 = ex.Message;
                                        __执行成功 = false;
                                    }
                                    __发送 = HJSON.序列化(new M属性值查询结果() { 成功 = __执行成功, 描述 = __执行描述, 返回值 = __返回值 });
                                    H日志输出.记录(string.Format("发送 {0}", __发送));
                                    return 文本编码(__发送);
                                }
                            }
                            return new byte[0];
                        default:
                            return new byte[0];
                    }
                case "css":
                    __上下文.Response.ContentType = "text/css;charset=utf-8";
                    break;
                case "js":
                    __上下文.Response.ContentType = "application/javascript;charset=utf-8";
                    break;
                default:
                    return new byte[0];
            }
            return 读取资源(string.Format("通用访问.WebUI.{0}", __文件名.Replace('\\','.')));
        }

        byte[] 文本编码(string __内容)
        {
            return Encoding.UTF8.GetBytes(__内容);
        }

        private byte[] 读取资源(string __路径)
        {
            var __stream =this.GetType().Assembly.GetManifestResourceStream(__路径);
            if (__stream == null)
            {
                return new byte[0];
            }
            var __结果 = new byte[__stream.Length];
            __stream.Read(__结果, 0, __结果.Length);
            return __结果;
        }
    }
}
