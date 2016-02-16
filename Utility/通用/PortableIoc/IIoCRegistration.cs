namespace PortableIoC
{
    internal interface IIoCRegistration
    {
        /// <summary>
        /// True if a shared instance has been created
        /// </summary>
        bool HasInstance { get; }

        /// <summary>
        /// Resolves the instance of a type
        /// </summary>
        /// <param name="ioc">The ioc container for chained dependencies</param>
        /// <param name="createNew">True for a non-shared instance</param>
        /// <returns>The resolved instance</returns>
        object GetInstance(IPortableIoC ioc, bool createNew);

        /// <summary>
        /// Destroy the shared instance 
        /// </summary>
        bool DestroyInstance();

        /// <summary>
        /// The type this registration represents
        /// </summary>
        string Type { get; }
    }

// ReSharper disable TypeParameterCanBeVariant
    internal interface IIoCRegistration<T> : IIoCRegistration
// ReSharper restore TypeParameterCanBeVariant
    {
        T GetTypedInstance(IPortableIoC ioc, bool createNew);
    }
}