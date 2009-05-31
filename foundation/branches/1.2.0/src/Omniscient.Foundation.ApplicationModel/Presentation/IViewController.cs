using System;
using System.Collections.Generic;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    /// <summary>
    /// Defines a view controller.
    /// 
    /// View controllers are responsible for controlling the display of individual views.
    /// </summary>
    public interface IViewController
    {
        /// <summary>
        /// Raised when the user switches the current view.
        /// </summary>
        event EventHandler CurrentViewChanged;

        /// <summary>
        /// Opens a view for the given model.
        /// </summary>
        /// <param name="model">Model to open in a view.</param>
        /// <returns>Newly opened view.</returns>
        IView OpenView(IModel model);

        /// <summary>
        /// Opens a view for the given list of models.
        /// </summary>
        /// <param name="models">Models to open in a view.</param>
        /// <returns>Newly opened view.</returns>
        IView OpenView<TModel>(IList<TModel> models)
            where TModel : IModel;

        /// <summary>
        /// Returns the view that has the focus, or by any other mean is the "current view".
        /// </summary>
        IView CurrentView { get; set; }

        /// <summary>
        /// Puts a certain view into focus, given that this view is already opened by the controller.
        /// </summary>
        /// <param name="view">The view that receives focus.</param>
        void Focus(IView view);

        /// <summary>
        /// Closes a certain view, given that this view is already opened by the controller.
        /// </summary>
        /// <param name="view">The view to close.</param>
        /// <returns>True if the view has been successfully closed.  Otherwise, false.</returns>
        bool CloseView(IView view);

        /// <summary>
        /// Closes a group of views, until a certain view is not already opened by the controller.
        /// </summary>
        /// <param name="views">The views to close.</param>
        /// <returns>True if all views have been closed.  Otherwise, false.</returns>
        bool CloseViewRange(IEnumerable<IView> views);
    }
}
