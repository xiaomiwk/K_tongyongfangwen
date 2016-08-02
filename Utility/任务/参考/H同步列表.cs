using System;
using System.Threading;
using System.Collections.Generic;

namespace Utility.任务
{
    public class H同步列表<K>
    {
        private readonly ReaderWriterLockSlim _锁 = new ReaderWriterLockSlim();

        private readonly List<K> _缓存 = new List<K>();

        public K 读取(int 索引)
        {
            _锁.EnterReadLock();
            try
            {
                return _缓存[索引];
            }
            finally
            {
                _锁.ExitReadLock();
            }
        }

        public List<K> 浅复制()
        {
            _锁.EnterReadLock();
            try
            {
                return new List<K>(_缓存);
            }
            finally
            {
                _锁.ExitReadLock();
            }
        }

        public void 添加(K 对象)
        {
            _锁.EnterWriteLock();
            try
            {
                _缓存.Add(对象);
            }
            finally
            {
                _锁.ExitWriteLock();
            }
        }

        public void 添加(IEnumerable<K> 对象列表)
        {
            _锁.EnterWriteLock();
            try
            {
                _缓存.AddRange(对象列表);
            }
            finally
            {
                _锁.ExitWriteLock();
            }
        }

        public bool 添加(K 对象, int 超时毫秒)
        {
            if (_锁.TryEnterWriteLock(超时毫秒))
            {
                try
                {
                    _缓存.Add(对象);
                }
                finally
                {
                    _锁.ExitWriteLock();
                }
                return true;
            }
            return false;
        }

        public void 插入(int 索引, K 对象)
        {
            _锁.EnterWriteLock();
            try
            {
                _缓存.Insert(索引, 对象);
            }
            finally
            {
                _锁.ExitWriteLock();
            }
        }

        public void 删除(K 对象)
        {
            _锁.EnterWriteLock();
            try
            {
                _缓存.Remove(对象);
            }
            finally
            {
                _锁.ExitWriteLock();
            }
        }

        public void 删除索引(int 索引)
        {
            _锁.EnterWriteLock();
            try
            {
                _缓存.RemoveAt(索引);
            }
            finally
            {
                _锁.ExitWriteLock();
            }
        }

        public void 删除全部()
        {
            _锁.EnterWriteLock();
            try
            {
                _缓存.Clear();
            }
            finally
            {
                _锁.ExitWriteLock();
            }
        }
    }
}

