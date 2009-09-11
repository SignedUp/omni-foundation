using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.ApplicationModel.Presentation.MVVM
{
    public interface IView : Omniscient.Foundation.ApplicationModel.Presentation.IView
    {
        IViewModel ViewModel { get; set; }
    }

    public interface IView<TModel> : IView
        where TModel: IModel
    {
        IViewModel<TModel> ViewModel { get; set; }
    }
}
