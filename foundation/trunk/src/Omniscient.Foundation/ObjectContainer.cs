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

        public ObjectContainer()
        {
            _store = new Dictionary<Type, object>();
        }

        #region IObjectContainer Members

        public void Register<TObject>(TObject instance)
        {
            if (_store.ContainsKey(typeof(TObject)))
                throw new ArgumentException(string.Format("Object of type {0} is already registered.", typeof(TObject)));

            _store.Add(typeof(TObject), instance);
        }

        public TObject Get<TObject>() 
        {
            if (!_store.ContainsKey(typeof(TObject))) return default(TObject);

            return (TObject)_store[typeof(TObject)];
        }

        #endregion
    }
}
