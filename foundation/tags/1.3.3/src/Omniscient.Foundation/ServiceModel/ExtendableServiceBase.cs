namespace Omniscient.Foundation.ServiceModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Omniscient.Foundation.ApplicationModel;

    /// <summary>
    /// Extendable service
    /// </summary>
    /// <typeparam name="TServiceContract">
    /// The Service contract which is going to be extended
    /// </typeparam>
    /// <typeparam name="TExtensionContract">
    /// The extension's contract
    /// </typeparam>
    public abstract class ExtendableServiceBase<TServiceContract, TExtensionContract>
        : ServiceBase<TServiceContract>, IExtendable<TExtensionContract>
    {
        private readonly List<TExtensionContract> _extenders;

        protected ExtendableServiceBase()
        {
            _extenders = new List<TExtensionContract>();
        }

        public IEnumerable<TExtensionContract> AllExtenders
        {
            get
            {
                foreach (TExtensionContract ext in _extenders) yield return ext;
            }
        }

        public void RegisterExtender(TExtensionContract extender)
        {
            _extenders.Add(extender);
        }

        public void UnregisterExtenders(Type type)
        {
            List<TExtensionContract> deletedTypes = new List<TExtensionContract>();
            deletedTypes = _extenders.Where(p => p.GetType() == type).ToList();

            foreach (var deletedType in deletedTypes)
            {
                _extenders.Remove(deletedType);
            }
        }
    }
}
