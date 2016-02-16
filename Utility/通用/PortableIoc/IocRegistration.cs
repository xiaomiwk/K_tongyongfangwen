using System;

namespace PortableIoC
{
    /// <summary>
    /// Represents the registration of a resolution
    /// </summary>
    /// <typeparam name="T">The type to be resolved</typeparam>
    internal class IocRegistration<T> : BaseIoCRegistration, IIoCRegistration<T>
    {
        private readonly int _hashCode;
        private readonly string _typeName;

        /// <summary>
        /// Constructor takes typed and passes through to base
        /// </summary>
        /// <param name="create">Create the </param>
        public IocRegistration(Func<IPortableIoC, T> create) : base(ioc => create(ioc))
        {
            var type = typeof (T).FullName;

            if (string.IsNullOrEmpty(type))
            {
                throw new InvalidOperationException("Cannot create a non-typed registration.");
            }

            _hashCode = type.GetHashCode();
            _typeName = type;
        }

        /// <summary>
        /// Grabs the typed implementation of the base instance
        /// </summary>
        /// <param name="ioc">The ioc container</param>
        /// <param name="createNew">True for a non-shared instance</param>
        /// <returns>The resolved instance</returns>
        public T GetTypedInstance(IPortableIoC ioc, bool createNew)
        {
            return (T) GetInstance(ioc, createNew);
        }

        /// <summary>
        /// The type this registration represents
        /// </summary>
        public override string Type
        {
            get { return _typeName; }
        }

        /// <summary>
        /// Equals will sort/filter based on type
        /// </summary>
        /// <param name="obj">The other object</param>
        /// <returns>True if the same type</returns>
        public override bool Equals(object obj)
        {
            return obj is IocRegistration<T> &&
                   ((IocRegistration<T>) obj).Type.Equals(Type);
        }

        /// <summary>
        /// Get the hash code of the type
        /// </summary>
        /// <returns>The type</returns>
        public override int GetHashCode()
        {
            return _hashCode;
        }
        
    }
}
