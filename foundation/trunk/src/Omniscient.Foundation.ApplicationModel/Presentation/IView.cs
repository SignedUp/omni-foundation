using System;
namespace Omniscient.Foundation.ApplicationModel.Presentation
{

    public delegate void ViewContextChangedEventHandler(object source, ViewContextChangedEventArgs e);
    
    public class ViewContextChangedEventArgs : EventArgs
    {
        public IView CurrentView { get; set; }
        public Object CurrentSelectedObject { get; set; }

        public ViewContextChangedEventArgs()
        { }

        public ViewContextChangedEventArgs(IView currentView)
            :this(currentView, null){}
        
        public ViewContextChangedEventArgs(IView currentView, object currentSelectedObject)
        {
            CurrentView = currentView;
            CurrentSelectedObject = currentSelectedObject;
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
        /// Raise when the selected item changed inside the current view.
        /// </summary>
        event ViewContextChangedEventHandler ViewContextChanged;

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
    }
}
