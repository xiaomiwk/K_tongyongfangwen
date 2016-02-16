using System;

namespace PortableIoC
{
    /// <summary>
    /// Helper for parameter validation
    /// </summary>
    public static class RequireThat
    {
        /// <summary>
        /// Require that a value is not null
        /// </summary>
        /// <typeparam name="T">The type of the value</typeparam>
        /// <param name="value">The value</param>
        /// <param name="parameterName">The name of the parameter that passed the value</param>
         public static void IsNotNull<T>(T value, string parameterName) where T: class
         {
             if (value == null)
             {
                throw new ArgumentNullException(parameterName);
             }
         }
    }
}