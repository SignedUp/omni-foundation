using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation
{
    /// <summary>
    /// Defines an object container where objects of certain types can be registered and later gathered.
    /// </summary>
    public interface IObjectContainer
    {
        /// <summary>
        /// Registers an object with the object container.  The object's type is used as the key.
        /// </summary>
        /// <typeparam name="TObject">Type of the registered object.  Used as the key.</typeparam>
        /// <param name="instance">Instance of object to store for later use.</param>
        void Register<TObject>(TObject instance);

        /// <summary>
        /// Registers an object with the object container.  Object must be of type <paramref name="type"/>.
        /// Type is used as the key.
        /// </summary>
        /// <param name="type">Type of the registered object.  Used as the key.</param>
        /// <param name="instance">Instance of object to store for later use.</param>
        void Register(Type type, object instance);

        /// <summary>
        /// Gets an object previously registered in the container.  Returns null if no object is found.
        /// </summary>
        /// <typeparam name="TObject">The type of the object to store.</typeparam>
        /// <returns>Registered object as a singleton.</returns>
        TObject Get<TObject>();

        /// <summary>
        /// Gets all registered objects as a flat array of objects.
        /// </summary>
        object[] AllObjects { get; }
    }
}
