using System;
using System.Collections.Generic;

namespace Omniscient.Foundation.ApplicationModel.Modularity
{
    ///<summary>
    ///</summary>
    ///<typeparam name="TContract"></typeparam>
    public class ExpandableBase<TContract>: IExpandable<TContract>
    {
        private List<IExtender<TContract>> _extenders;

        ///<summary>
        ///</summary>
        public ExpandableBase()
        {
            _extenders = new List<IExtender<TContract>>();
        }

        public void Register(IExtender<TContract> extender)
        {
            _extenders.Add(extender);
        }

        public IEnumerable<IExtender<TContract>> Extenders
        {
            get { return _extenders.ToArray(); }
        }
    }

}
