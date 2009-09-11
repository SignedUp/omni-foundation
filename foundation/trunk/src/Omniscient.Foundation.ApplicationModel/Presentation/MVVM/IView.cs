using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.ApplicationModel.Presentation.MVVM
{
    public interface IView<TModel> : Omniscient.Foundation.ApplicationModel.Presentation.IView
        where TModel: IModel
    {
        IViewModel<TModel> ViewModel { get; set; }
    }
}
