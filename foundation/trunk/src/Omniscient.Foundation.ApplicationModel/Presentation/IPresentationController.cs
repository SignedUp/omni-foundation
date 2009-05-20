using System;
using System.Collections.Generic;
using Omniscient.Foundation.Data;

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
        /// Informs the controller that a view has been closed.
        /// </summary>
        /// <param name="view">The view that's been closed.</param>
        void ViewClosed(IView view);

        /// <summary>
        /// Registers an <c>IViewController</c>. Default use is to feed that list at application startup.
        /// </summary>
        /// <param name="controller">An <c>IViewController</c> to register.</param>
        void RegisterViewController(IViewController controller);

        /// <summary>
        /// Gets an <c>IViewController</c> using its type.
        /// </summary>
        /// <typeparam name="ViewControllerType">The type of the view controller to look for.</typeparam>
        /// <returns>An <c>IViewController</c> if found.  Otherwise, null.</returns>
        ViewControllerType GetViewController<ViewControllerType>() where ViewControllerType : IViewController;


        /// <summary>
        /// Registers an <c>IPresenter</c>.
        /// </summary>
        /// <param name="presenter">An <c>IPresenter</c> to register.</param>
        /// <exception cref="InvalidOperationException">Thrown if the <c>IPresenter.RequiresUserInput</c> is
        /// <c>True</c>, while the value of <c>SupportsUserInput</c> is <c>False</c>.</exception>
        void RegisterPresenter(IPresenter presenter);

        /// <summary>
        /// Gets an <c>IPresenter</c> using its name.
        /// </summary>
        /// <param name="name">The name of the presenter to look for.</param>
        /// <returns>An <c>IPresenter</c> if found.  Otherwise, null.</returns>
        IPresenter GetPresenter(string name);

        /// <summary>
        /// Gets an <c>IPresenter</c> using its type.
        /// </summary>
        /// <typeparam name="PresenterType">The type of the presenter to look for.</typeparam>
        /// <returns>An <c>IPresenter</c> if found.  Otherwise, null.</returns>
        PresenterType GetPresenter<PresenterType>() where PresenterType : IPresenter;

        /// <summary>
        /// Gets a value indicating wheter this presentation controller supports user input.
        /// If <c>False</c>, then calling <c>RegisterPresenter</c> with an <c>IPresenter</c>
        /// that requires user input will fail.
        /// </summary>
        bool SupportsUserInput { get; }

        /// <summary>
        /// Close all views of all view controllers.
        /// </summary>
        void CloseAllViews();

    }
}
