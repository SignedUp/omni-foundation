using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Omniscient.Foundation.ApplicationModel.Modularity;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    public class DashBoardViewController : IViewController
    {
        private IView _current;
        private DashBoard _dash;
        private IExtensionPort<IDashBoardViewControllerExtenderContract> _extensionPort;

        public DashBoardViewController(DashBoard dash)
        {
            _dash = dash;
            _extensionPort = new ExtensionPortBase<IDashBoardViewControllerExtenderContract>();
            ApplicationManager.Current.ObjectContainer.Register(_extensionPort);
        }


        #region IViewController Members

        public IView OpenView(IModel model)
        {
            IView view;
            foreach (IExtender<IDashBoardViewControllerExtenderContract> extender in _extensionPort.Extenders)
            {
                view = extender.GetImplementation().GetView(model);
                if (view != null) continue;
            }

            if (view == null) return null;
            view.Model = model;            
            _dash.OpenPanel(view);
            
            CurrentView = view;
            return view;
        }

        public IView CurrentView 
        {
            get
            {
                return _current;
            }
            set
            {
                if(_current == value) return;

                _current = value;
                
                if (CurrentViewChanged != null)
                    CurrentViewChanged(this, EventArgs.Empty);
            }
        }

        public event EventHandler CurrentViewChanged;

        #endregion
    }
}
