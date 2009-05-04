using System;
namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    /// <summary>
    /// Describes an event handler that handles selection changes that occur from within a view.
    /// </summary>
    /// <param name="source">The source of the event (generally an IView)</param>
    /// <param name="e">Event args</param>
    public delegate void SelectionChangedEventHandler(object source, SelectionChangedEventArgs e);
    
    /// <summary>
    /// Arguments passed to a SelectionChanged event raised by an IView.
    /// </summary>
    public class SelectionChangedEventArgs : EventArgs
    {
        public IView View { get; set; }
        public Object Selection { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        public SelectionChangedEventArgs()
        { }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="view">The view that raises the event</param>
        public SelectionChangedEventArgs(IView view)
            :this(view, null){}
        
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="view">The view that raises the event</param>
        /// <param name="selection">The object being selected in the view</param>
        public SelectionChangedEventArgs(IView view, object selection)
        {
            View = view;
            Selection = selection;
        }
    }

    /// <summary>
    /// Represents a view.
    /// 
    /// Views are responsible for displaying <see cref="IModel"/> objects to the UI.
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Raised when the selected item changed inside the current view.
        /// </summary>
        event SelectionChangedEventHandler SelectionChanged;

        /// <summary>
        /// Gets or sets the Model.
        /// </summary>
        IModel Model { get; set; }
        
        /// <summary>
        /// Called when the Model is modified from the outside of the View (probably in another view).
        /// </summary>
        void UpdateView();

        /// <summary>
        /// Gets or sets the view's title.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Gets an object that is currently selected by the user.  Works in pair with ViewContextChangedEventHandler.
        /// </summary>
        object Selection { get; }
    }
}
