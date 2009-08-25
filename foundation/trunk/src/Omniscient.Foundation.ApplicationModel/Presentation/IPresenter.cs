namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    /// <summary>
    /// The IPresenter gives direct access to the UI.  Contrary to <see href="IViewController"</see>s, which are the entry door to the MVC,
    /// IPresenter are there simply to display message windows, ask for user input, etc.
    /// </summary>
    public interface IPresenter
    {
        /// <summary>
        /// Gets the name of that presenter.  Generally the name of the concrete class.
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// Gets a values indicating whether this presenter will require user input.  For example, if the presenter
        /// is to display message windows with an Ok button, then it requires user input.  If it simply writes messages
        /// to a console window, or to a status bar, for example, then it does not require user input.  According to the 
        /// value of <see href="IPresentationController.SupportsUserInput"</see>, the presenter may not be allowed to be registered.
        /// </summary>
        bool RequiresUserInput { get; }

        /// <summary>
        /// Writes a single message to the presentation medium.  Most presenters will add more complex functionality (through
        /// a custom interface) than this simple function.
        /// </summary>
        /// <param name="message">A message to display.</param>
        void WriteMessage(object message);
    }
}
