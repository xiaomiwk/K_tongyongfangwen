using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace PortableIoC
{
    public class PortableIoc : IPortableIoC
    {
        private static readonly string DefaultLabel = Guid.NewGuid().ToString();

        private const string InvalidRegistration =
            "A definition for label {0} and type {1} already exists. Unregister the definition first.";

        private const string InvalidResolution =
            "Cannot resolve type {0} for label {1}.";

        private readonly IDictionary<string, IList<IIoCRegistration>> _containers =
            new Dictionary<string, IList<IIoCRegistration>>();

        private readonly object _mutex = new object();
        
        #region IPortableIoC Members

        /// <summary>
        /// Register a type to be created with an alias to create that includes an
        /// instance of the IoC container
        /// </summary>
        /// <typeparam name="T">The type that is going to be implemented</typeparam>
        /// <param name="label">A unique label that allows multiple implementations of the same type</param>
        /// <param name="creation">An expression to create a new instance of the type</param>
        public void Register<T>(Func<IPortableIoC, T> creation, string label = "")
        {
            label = string.IsNullOrEmpty(label) ? DefaultLabel : label;
            
            IsNotNull(creation, "creation");
            
            CheckContainer(label);
            
            Monitor.Enter(_mutex);

            try
            {
                if (Exists(label, typeof(T)))
                {
                    throw new InvalidOperationException(
                        string.Format(
                            InvalidRegistration,
                            label,
                            typeof(T).FullName));
                }
                var iocRegistration = new IocRegistration<T>(creation);
                _containers[label].Add(iocRegistration);
            }
            finally
            {
                Monitor.Exit(_mutex);
            }
        }

        /// <summary>
        /// Resolve the implementation of a type (interface, abstract class, etc.)
        /// </summary>
        /// <typeparam name="T">The type to resolve the implementation for</typeparam>
        /// <param name="label">A unique label that allows multiple implementations of the same type</param>
        /// <returns>The implementation (defaults to a shared implementation)</returns>
        public T Resolve<T>(string label = "")
        {
            label = string.IsNullOrEmpty(label) ? DefaultLabel : label;

            return Resolve<T>(false, label);
        }

        /// <summary>
        /// Overload to resolve the implementation of a type with the option to create
        /// a non-shared instance
        /// </summary>
        /// <typeparam name="T">The type to resolve</typeparam>
        /// <param name="label">A unique label that allows multiple implementations of the same type</param>
        /// <param name="createNew">True for a non-shared instance</param>
        /// <returns>The implementation of the type</returns>
        public T Resolve<T>(bool createNew, string label = "")
        {
            label = string.IsNullOrEmpty(label) ? DefaultLabel : label;

            CheckContainer(label);
            
            if (!Exists(label, typeof(T)))
            {
                throw new InvalidOperationException(
                    string.Format(
                    InvalidResolution,
                    typeof(T).FullName,
                    label));
            }

            lock (_mutex)
            {
                var iocRegistration = _containers[label].FirstOrDefault(ioc => ioc.Type == typeof (T).FullName)
                                      as IocRegistration<T>;

                if (iocRegistration == null)
                {
                    throw new InvalidOperationException(
                        string.Format(
                            InvalidResolution,
                            typeof (T).FullName,
                            label));
                }

                return iocRegistration.GetTypedInstance(this, createNew);
            }
        }

        /// <summary>
        /// Test to see whether it is possible to resolve a type
        /// </summary>
        /// <typeparam name="T">The type to test for</typeparam>
        /// <param name="label">A unique label that allows for multiple implementations of the same type</param>
        /// <returns>True if an implementation exists for the type</returns>
        public bool CanResolve<T>(string label = "")
        {
            label = string.IsNullOrEmpty(label) ? DefaultLabel : label;

            return Exists(label, typeof(T));
        }

        /// <summary>
        /// Attempt to resolve an instance
        /// </summary>
        /// <typeparam name="T">The type to resolve</typeparam>
        /// <param name="instance">The instance, if it is possible to create one</param>
        /// <param name="label">A unique label that allows for multiple implementations of the same type</param>
        /// <returns>True if the resolution succeeded</returns>
        public bool TryResolve<T>(out T instance, string label = "")
        {
            label = string.IsNullOrEmpty(label) ? DefaultLabel : label;
            T passThroughInstance;
            var result = TryResolve(out passThroughInstance, false, label);
            instance = passThroughInstance;
            return result;
        }

        /// <summary>
        /// Attempt to resolve an instance
        /// </summary>
        /// <typeparam name="T">The type to resolve</typeparam>
        /// <param name="instance">The instance, if it is possible to create one</param>
        /// <param name="createNew">Set to true to create a non-shared instance</param>
        /// <param name="label">A unique label that allows for multiple implementations of the same type</param>
        /// <returns>True if the resolution succeeded</returns>
        public bool TryResolve<T>(out T instance, bool createNew, string label = "")
        {
            label = string.IsNullOrEmpty(label) ? DefaultLabel : label;

            if (!Exists(label, typeof(T)))
            {
                instance = default(T);
                return false;
            }

            try
            {
                instance = Resolve<T>(createNew, label);
                return true;
            }
            catch
            {
                instance = default(T);
                return false;
            }
        }

        /// <summary>
        /// Unregister a definition. 
        /// </summary>
        /// <typeparam name="T">The type to destroy</typeparam>
        /// <param name="label">An optional label to allow multiple implementations of the same type</param>
        /// <returns>True if the definition existed</returns>
        /// <remarks>This will destroy references to any shared instances for the type</remarks>
        public bool Unregister<T>(string label = "")
        {
            label = string.IsNullOrEmpty(label) ? DefaultLabel : label;

            if (!Exists(label, typeof(T)))
            {
                return false;
            }

            Monitor.Enter(_mutex);

            try
            {
                if (!Exists(label, typeof(T)))
                {
                    return false;
                }
                var registration = _containers[label].First(c => c.Type == typeof (T).FullName);
                _containers[label].Remove(registration);
                return true;
            }
            finally
            {
                Monitor.Exit(_mutex);
            }
        }

        /// <summary>
        /// Destroy the shared instance of a type 
        /// </summary>
        /// <typeparam name="T">The type to destroy</typeparam>
        /// <param name="label">An optional label to allow multiple implementations of the same type</param>
        /// <returns>True if a shared instance existed</returns>
        /// <remarks>This will allow for a new instance to become shared if the resolve call is made later</remarks>
        public bool Destroy<T>(string label = "")
        {
            label = string.IsNullOrEmpty(label) ? DefaultLabel : label;

            if (!Exists(label, typeof(T)))
            {
                return false;
            }
            
            Monitor.Enter(_mutex);
            
            try
            {
                var registration = _containers[label].FirstOrDefault(c => c.Type == typeof(T).FullName);
                return registration != null && registration.DestroyInstance();
            }
            finally
            {
                Monitor.Exit(_mutex);
            }
        }

        #endregion

        /// <summary>
        /// Ensure a container exists for the given label
        /// </summary>
        /// <param name="label">A label to sub-divide containers</param>
        private void CheckContainer(string label)
        {
            IsNotNull(label, "label");

            if (_containers.ContainsKey(label))
            {
                return;
            }

            Monitor.Enter(_mutex);

            try
            {
                if (_containers.ContainsKey(label))
                {
                    return;
                }

                _containers.Add(label, new List<IIoCRegistration>());

                // every container can resolve the IOC parent
                var iocRegistration = new IocRegistration<IPortableIoC>(ioc => this);
                _containers[label].Add(iocRegistration);
            }
            finally
            {
                Monitor.Exit(_mutex);
            }
        }

        private bool Exists(string label, Type t)
        {
            IsNotNull(label, "label");
            IsNotNull(t, "t");

            if (!_containers.ContainsKey(label))
            {
                return false;
            }

            var type = t.FullName;

            lock (_mutex)
            {
                return _containers[label].Any(ioc => ioc.Type.Equals(type));
            }
        }

        private static void IsNotNull<T>(T value, string parameterName) where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

    }
}