using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    public abstract class DashBoardViewController : IViewController
    {
        private IView _current;
        private DashBoard _dash;

        public DashBoardViewController(DashBoard dash)
        {
            _dash = dash;
        }


        #region IViewController Members

        public IView OpenView(IModel model)
        {
            IView view;
            view = GetView(model);
            _dash.OpenPanel(view);
            return view;
        }

        protected abstract IView GetView(IModel model);

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
