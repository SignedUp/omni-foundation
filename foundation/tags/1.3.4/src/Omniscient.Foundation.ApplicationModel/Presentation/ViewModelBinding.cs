using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    /// <summary>
    /// Holds a binding between a model and an view.
    /// </summary>
    public class ViewModelBinding
    {
        /// <summary>
        /// Creates a binding for a type of model.
        /// </summary>
        /// <param name="modelType">The type of model to bind.</param>
        public ViewModelBinding(Type modelType)
        {
            ModelType = modelType;
        }

        /// <summary>
        /// The type of model that is bound.
        /// </summary>
        public Type ModelType { get; private set; }

        /// <summary>
        /// The binding to the view.
        /// </summary>
        public ViewViewModelBinding Binding { get; private set; }

        /// <summary>
        /// Binds a model to a view
        /// </summary>
        /// <typeparam name="TView">The type of view to bind to.</typeparam>
        /// <returns>A binding.</returns>
        public ViewViewModelBinding To<TView>()
        {
            if (Binding == null) Binding = new ViewViewModelBinding(typeof(TView));
            return Binding;
        }
    }
}
