using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Omniscient.Foundation.ApplicationModel.Presentation;
using Omniscient.Foundation.Commanding;

namespace Omniscient.Foundation.Contrib.Wpf
{
    public class ObservableHierarchicalCommandObject : IPresentableHierarchicalObject<ObservableHierarchicalCommandObject>, IPresentableCommandObject
    {
        public ObservableHierarchicalCommandObject()
        {
            this.Children = new ObservableCollection<ObservableHierarchicalCommandObject>();
            this.Text = string.Empty;
        }

        #region IPresentableHierarchicalObject<HierarchicalCommandObject> Members

        IEnumerable<ObservableHierarchicalCommandObject> IPresentableHierarchicalObject<ObservableHierarchicalCommandObject>.Children
        {
            get { return this.Children; }
        }

        public ObservableCollection<ObservableHierarchicalCommandObject> Children
        {
            get;
            private set;
        }

        #endregion

        #region IPresentableObject Members

        public string Text
        {
            get;
            set;
        }

        #endregion

        #region IPresentableCommandObject Members

        public ICommandCore Command
        {
            get;
            set;
        }

        #endregion
    }
}
