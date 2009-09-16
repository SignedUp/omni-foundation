using System;
using System.Collections.Generic;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    /// <summary>
    /// Represents an application controller.  Responsible for managing entity edition concurrency, and for managing the UI in general.
    /// </summary>
    public interface IPresentationController
    {
        /// <summary>
        /// Opens a view.  The controller is responsible for finding a view for that model, instanciating the view
        /// and displaying it correctly.
        /// </summary>
        /// <param name="model">The model to open.</param>
        void OpenView(IModel model);

        /// <summary>
        /// Opens a view.  The controller is responsible for finding a view for those models, instanciating the view
        /// and displaying it correctly.
        /// </summary>
        /// <param name="models">The models to open.</param>
        void OpenView<TModel>(IList<TModel> models)
            where TModel : IModel;

        /// <summary>
        /// Informs the controller that a view has been closed.
        /// </summary>
        /// <param name="view">The view that's been closed.</param>
        void ViewClosed(IView view);

        /// <summary>
        /// Registers an <see href="IViewController"</see>. Default use is to feed that list at application startup.
        /// </summary>
        /// <param name="controller">An <see href="IViewController"</see> to register.</param>
        void RegisterViewController(IViewController controller);

        /// <summary>
        /// Gets an <see href="IViewController"</see> using its type.
        /// </summary>
        /// <typeparam name="TViewController">The type of the view controller to look for.</typeparam>
        /// <returns>An <see href="IViewController"</see> if found.  Otherwise, null.</returns>
        TViewController GetViewController<TViewController>()
            where TViewController : IViewController;


        /// <summary>
        /// Registers an <see href="IPresenter"</see>.
        /// </summary>
        /// <param name="presenter">An <see href="IPresenter"</see> to register.</param>
        /// <exception cref="InvalidOperationException">Thrown if the <see href="IPresenter.RequiresUserInput"</see> is
        /// <see href="True"</see>, while the value of <see href="SupportsUserInput"</see> is <see href="False"</see>.</exception>
        void RegisterPresenter(IPresenter presenter);

        /// <summary>
        /// Gets an <see href="IPresenter"</see> using its name.
        /// </summary>
        /// <param name="name">The name of the presenter to look for.</param>
        /// <returns>An <see href="IPresenter"</see> if found.  Otherwise, null.</returns>
        IPresenter GetPresenter(string name);

        /// <summary>
        /// Gets an <see href="IPresenter"</see> using its type.
        /// </summary>
        /// <typeparam name="TPresenter">The type of the presenter to look for.</typeparam>
        /// <returns>An <see href="IPresenter"</see> if found.  Otherwise, null.</returns>
        TPresenter GetPresenter<TPresenter>()
            where TPresenter : IPresenter;

        /// <summary>
        /// Gets a value indicating wheter this presentation controller supports user input.
        /// If <see href="False"</see>, then calling <see href="RegisterPresenter"</see> with an <see href="IPresenter"</see>
        /// that requires user input will fail.
        /// </summary>
        bool SupportsUserInput { get; }

        /// <summary>
        /// Close all views of all view controllers.
        /// </summary>
        void CloseAllViews();

        ///<summary>
        /// Closes all views for a specific model type.
        ///</summary>
        ///<typeparam name="TModel">Model type filter.</typeparam>
        void CloseAllViews<TModel>() where TModel : IModel;
    }
}
