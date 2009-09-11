using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.ApplicationModel.Presentation.MVVM
{
    public interface IViewModel
    {
        IModel Model { get; set; }
    }

    public interface IViewModel<TModel> : IViewModel 
        where TModel : IModel
    {
        TModel Model { get; set; }
    }
}
