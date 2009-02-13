using System.Collections.Generic;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    /// <summary>
    /// The shell is the main window in an application.  It defines placeholders for displaying <c>IModel</c> objects,
    /// which wrap <c>IEntity</c> objects, and other placeholder for displaying non-data information (like commands) through the
    /// use of <c>IPresenter</c> objects.
    /// </summary>
    public interface IShell
    {
        /// <summary>
        /// Tells the shell to display itself, when the application starts.
        /// </summary>
        void Show();

        /// <summary>
        /// Asks the shell to create one <c>IViewController</c> object for each region where <c>IModel</c> objects
        /// will be displayed, and return all created controllers.  The shell does not have to keep a reference to
        /// those controllers.
        /// </summary>
        /// <remarks>
        /// In most applications, the shell will have only one view controllers.
        /// </remarks>
        /// <returns>The list of created view controllers.</returns>
        IEnumerable<IViewController> CreateViewControllers();

        /// <summary>
        /// Asks the shell to create all non-data presenters (e.g. Status Bar Presenter, or Message Console Presenter).
        /// The shell does not have to keep a reference to those presenters.
        /// </summary>
        /// <returns>The list of created presenters.</returns>
        IEnumerable<IPresenter> CreatePresenters();
    }
}
