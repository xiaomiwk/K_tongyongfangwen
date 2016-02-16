using System;

namespace PortableIoC
{
    /// <summary>
    /// Lightweight, portable Inversion of Control container that will work on
    /// .NET Framework 4.x, Silverlight 4.0 and 5.0, WPF, Windows Phone 7.x and later,
    /// and Windows Store (Windows 8) applications
    /// </summary>
    public interface IPortableIoC
    {
        /// <summary>
        /// Register a type to be created with an alias to create that includes an
        /// instance of the IoC container
        /// </summary>
        /// <typeparam name="T">The type that is going to be implemented</typeparam>
        /// <param name="label">A unique label that allows multiple implementations of the same type</param>
        /// <param name="creation">An expression to create a new instance of the type</param>
        void Register<T>(Func<IPortableIoC, T> creation, string label = "");

        /// <summary>
        /// Resolve the implementation of a type (interface, abstract class, etc.)
        /// </summary>
        /// <typeparam name="T">The type to resolve the implementation for</typeparam>
        /// <param name="label">A unique label that allows multiple implementations of the same type</param>
        /// <returns>The implementation (defaults to a shared implementation)</returns>
        T Resolve<T>(string label = "");

        /// <summary>
        /// Overload to resolve the implementation of a type with the option to create
        /// a non-shared instance
        /// </summary>
        /// <typeparam name="T">The type to resolve</typeparam>
        /// <param name="label">A unique label that allows multiple implementations of the same type</param>
        /// <param name="createNew">True for a non-shared instance</param>
        /// <returns>The implementation of the type</returns>
        T Resolve<T>(bool createNew, string label = "");

        /// <summary>
        /// Test to see whether it is possible to resolve a type
        /// </summary>
        /// <typeparam name="T">The type to test for</typeparam>
        /// <param name="label">A unique label that allows for multiple implementations of the same type</param>
        /// <returns>True if an implementation exists for the type</returns>
        bool CanResolve<T>(string label = "");

        /// <summary>
        /// Attempt to resolve an instance
        /// </summary>
        /// <typeparam name="T">The type to resolve</typeparam>
        /// <param name="instance">The instance, if it is possible to create one</param>
        /// <param name="label">A unique label that allows for multiple implementations of the same type</param>
        /// <returns>True if the resolution succeeded</returns>
        bool TryResolve<T>(out T instance, string label = "");

        /// <summary>
        /// Attempt to resolve an instance
        /// </summary>
        /// <typeparam name="T">The type to resolve</typeparam>
        /// <param name="instance">The instance, if it is possible to create one</param>
        /// <param name="createNew">Set to true to create a non-shared instance</param>
        /// <param name="label">A unique label that allows for multiple implementations of the same type</param>
        /// <returns>True if the resolution succeeded</returns>
        bool TryResolve<T>(out T instance, bool createNew, string label = "");

        /// <summary>
        /// Unregister a definition. 
        /// </summary>
        /// <typeparam name="T">The type to destroy</typeparam>
        /// <param name="label">An optional label to allow multiple implementations of the same type</param>
        /// <returns>True if the definition existed</returns>
        /// <remarks>This will destroy references to any shared instances for the type</remarks>
        bool Unregister<T>(string label = "");

        /// <summary>
        /// Destroy the shared instance of a type 
        /// </summary>
        /// <typeparam name="T">The type to destroy</typeparam>
        /// <param name="label">An optional label to allow multiple implementations of the same type</param>
        /// <returns>True if a shared instance existed</returns>
        /// <remarks>This will allow for a new instance to become shared if the resolve call is made later</remarks>
        bool Destroy<T>(string label = "");
    }
}