using System;

namespace Omniscient.Foundation.ServiceModel
{
    /// <summary>
    /// Defines the base interface for a service.
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// Gets the implementation's contract type of the service.
        /// </summary>
        Type ImplementationType { get; }

        /// <summary>
        /// Gets the implementation of the service.  Returns an object whose type must be equal to <c>ImplementationType</c>.
        /// </summary>
        /// <returns>Service's implementation, of type <c>ImplementationType</c>.</returns>
        object GetImplementation();
    }
}
