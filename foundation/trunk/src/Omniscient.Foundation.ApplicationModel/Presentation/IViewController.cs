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
        /// Returns the view that has the focus, or by any other mean is the "current view".
        /// </summary>
        IView CurrentView { get; set; }

        void Focus(IView view);
        bool CloseView(IView view);
        bool CloseViewRange(IEnumerable<IView> views);
    }
}
