using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Omniscient.Foundation.Data;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    public interface IViewController
    {
        IView OpenView(IModel model);
        IView CurrentView { get; set; }
        event EventHandler CurrentViewChanged;
    }
}
