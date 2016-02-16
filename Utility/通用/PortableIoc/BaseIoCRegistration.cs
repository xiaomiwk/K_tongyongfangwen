using System;
using System.Threading;

namespace PortableIoC
{
    /// <summary>
    /// Base class to track a registration instance
    /// </summary>
    internal abstract class BaseIoCRegistration : IIoCRegistration
    {
        /// <summary>
        /// The shared instance
        /// </summary>
        private object _instance;

        /// <summary>
        /// Lock
        /// </summary>
        private readonly object _mutex = new object();

        /// <summary>
        /// Delegate that describes how to create a new instance
        /// </summary>
        private readonly Func<IPortableIoC, object> _creation;

        /// <summary>
        /// Constructor takes in the instructions to create the instance
        /// </summary>
        /// <param name="create">A delegate for instance creation</param>
        internal BaseIoCRegistration(Func<IPortableIoC, object> create)
        {
            _creation = create;
            HasInstance = false;
        }

        /// <summary>
        /// Destroy the shared instance
        /// </summary>
        public bool DestroyInstance()
        {
            if (!HasInstance)
            {
                return false;
            }
            
            Monitor.Enter(_mutex);

            try
            {
                if (!HasInstance)
                {
                    return false;
                }

                _instance = null;
                HasInstance = false;
                return true;
            }
            finally
            {
                Monitor.Exit(_mutex);
            }
        }

        /// <summary>
        /// The type this registration implements
        /// </summary>
        public abstract string Type { get; }

        /// <summary>
        /// Whether a shared instance has been created or not
        /// </summary>
        public bool HasInstance { get; private set; }

        /// <summary>
        /// Get the instance
        /// </summary>
        /// <param name="ioc">The ioc container</param>
        /// <param name="createNew">True for a non-shared instance</param>
        /// <returns>The resolved instance</returns>
        public object GetInstance(IPortableIoC ioc, bool createNew)
        {
            if (createNew)
            {
                return _creation(ioc);
            }

            if (HasInstance)
            {
                return _instance;
            }

            Monitor.Enter(_mutex);

            try
            {
                if (HasInstance)
                {
                    return _instance;
                }

                _instance = _creation(ioc);
                HasInstance = true;

                return _instance;
            }
            finally
            {
                Monitor.Exit(_mutex);    
            }            
        }
    }
}
