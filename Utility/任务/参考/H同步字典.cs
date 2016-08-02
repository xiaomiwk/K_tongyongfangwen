using System;
using System.Threading;
using System.Collections.Generic;

namespace Utility.任务
{
    /// <summary>
    /// 基于Dictionary的同步实现，源自MSDN中  ReaderWriterLockSlim 的事例，同时可以参考System.Collections.Concurrent.ConcurrentDictionary<TKey, TValue>
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class H同步字典<K, V>
    {
        private readonly ReaderWriterLockSlim _锁 = new ReaderWriterLockSlim();

        private readonly Dictionary<K, V> _缓存 = new Dictionary<K, V>();

        public V 读取(K key)
        {
            _锁.EnterReadLock();
            try
            {
                return _缓存[key];
            }
            finally
            {
                _锁.ExitReadLock();
            }
        }

        public void 添加(K key, V value)
        {
            _锁.EnterWriteLock();
            try
            {
                _缓存.Add(key, value);
            }
            finally
            {
                _锁.ExitWriteLock();
            }
        }

        public bool 添加(K key, V value, int timeout)
        {
            if (_锁.TryEnterWriteLock(timeout))
            {
                try
                {
                    _缓存.Add(key, value);
                }
                finally
                {
                    _锁.ExitWriteLock();
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public E添加或更新 添加或更新(K key, V value)
        {
            _锁.EnterUpgradeableReadLock();
            try
            {
                V result = default(V);
                if (_缓存.TryGetValue(key, out result))
                {
                    if (result.Equals(value))
                    {
                        return E添加或更新.Unchanged;
                    }
                    else
                    {
                        _锁.EnterWriteLock();
                        try
                        {
                            _缓存[key] = value;
                        }
                        finally
                        {
                            _锁.ExitWriteLock();
                        }
                        return E添加或更新.Updated;
                    }
                }
                else
                {
                    _锁.EnterWriteLock();
                    try
                    {
                        _缓存.Add(key, value);
                    }
                    finally
                    {
                        _锁.ExitWriteLock();
                    }
                    return E添加或更新.Added;
                }
            }
            finally
            {
                _锁.ExitUpgradeableReadLock();
            }
        }

        public void 删除(K key)
        {
            _锁.EnterWriteLock();
            try
            {
                _缓存.Remove(key);
            }
            finally
            {
                _锁.ExitWriteLock();
            }
        }

        public enum E添加或更新
        {
            Added,
            Updated,
            Unchanged
        };
    }
}

