using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Utility.存储
{
    public interface I文件存储_单对象<T> where T : class
    {
        void 初始化路径(string __存储路径 = null);

        bool 存在 { get; }

        T 查询();

        void 设置(T item);

        void 删除(T item);

        void 刷新缓存();
    }

    public class H文件存储_单对象<T> : I文件存储_单对象<T> where T : class
    {
        protected string _存储路径;

        protected T _缓存数据;

        protected readonly object _同步对象 = new object();

        private bool _已初始化 = false;

        protected void 检测初始化()
        {
            if (!_已初始化)
            {
                初始化路径();
            }
        }

        public void 初始化路径(string __存储路径 = null)
        {
            if (__存储路径 == null)
            {
                __存储路径 = string.Format("存储\\{0}", typeof(T).Name);
            }
            _存储路径 = __存储路径;
            _缓存数据 = (T)H文件存储.二进制读取(_存储路径);
            _已初始化 = true;
        }

        public bool 存在
        {
            get
            {
                检测初始化();
                return _缓存数据 != null; 
            }
        }

        public T 查询()
        {
            检测初始化();
            return _缓存数据;
        }

        public void 设置(T item)
        {
            检测初始化();

            lock (_同步对象)
            {
                _缓存数据 = item;
                H文件存储.二进制存储(_缓存数据, _存储路径);
            }
        }

        public void 删除(T item)
        {
            检测初始化();

            if (存在)
            {
                _缓存数据 = null;
                File.Delete(H路径.获取绝对路径(_存储路径));
            }
        }

        /// <summary>
        /// 当读写程序不一致时,需要用到
        /// </summary>
        public virtual void 刷新缓存()
        {
            检测初始化();

            _缓存数据 = (T)H文件存储.二进制读取(_存储路径);
        }

    }
}
