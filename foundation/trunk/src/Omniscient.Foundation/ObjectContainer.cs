using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation
{
    /// <summary>
    /// Default implementation for <see cref="IObjectContainer"/>.
    /// </summary>
    public class ObjectContainer: IObjectContainer
    {
        private Dictionary<Type, object> _store;

        /// <summary>
        /// Ctor.
        /// </summary>
        public ObjectContainer()
        {
            _store = new Dictionary<Type, object>();
        }

        #region IObjectContainer Members

        /// <summary>
        /// Registers an object with the object container.  The object's type is used as the key.
        /// </summary>
        /// <typeparam name="TObject">Type of the registered object.  Used as the key.</typeparam>
        /// <param name="instance">Instance of object to store for later use.</param>
        public void Register<TObject>(TObject instance)
        {
            this.Register(typeof(TObject), instance);
        }

        public void Register(Type type, object instance)
        {
            if (_store.ContainsKey(type))
                throw new ArgumentException(string.Format("Object of type {0} is already registered.", type));

            if (!type.IsAssignableFrom(instance.GetType()))
                throw new InvalidCastException(string.Format("Cannot cast object of type {0} to type {1}.", instance.GetType(), type));

            _store.Add(type, instance);
        }

        /// <summary>
        /// Gets an object previously registered in the container.  Returns null if no object is found.
        /// </summary>
        /// <typeparam name="TObject">The type of the object to store.</typeparam>
        /// <returns>Registered object as a singleton.</returns>
        public TObject Get<TObject>() 
        {
            if (!_store.ContainsKey(typeof(TObject))) return default(TObject);

            return (TObject)_store[typeof(TObject)];
        }

        /// <summary>
        /// Gets all registered objects as a flat array of objects.
        /// </summary>
        public object[] AllObjects
        {
            get { return _store.Values.ToArray(); }
        }

        public void Clear()
        {
            _store.Clear();
        }

        #endregion
    }
}
