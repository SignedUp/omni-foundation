using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    /// <summary>
    /// Represents a binding between a model, a view and optionally a view model.
    /// </summary>
    public class ViewViewModelBinding
    {
        /// <summary>
        /// Creates the binding
        /// </summary>
        /// <param name="viewType">The type of the view that is bound.</param>
        public ViewViewModelBinding(Type viewType)
        {
            ViewType = viewType;
        }

        /// <summary>
        /// Gets the type of the view that is bound.
        /// </summary>
        public Type ViewType { get; private set; }

        /// <summary>
        /// Gets the optional view model that is bound.
        /// </summary>
        public Type ViewModelType { get; private set; }

        /// <summary>
        /// Binds a view model to the model-view binding.
        /// </summary>
        /// <typeparam name="TViewModel">The type of view model to bind.</typeparam>
        /// <returns>Returns this.</returns>
        public ViewViewModelBinding Through<TViewModel>()
        {
            ViewModelType = typeof(TViewModel);
            return this;
        }
    }
}
