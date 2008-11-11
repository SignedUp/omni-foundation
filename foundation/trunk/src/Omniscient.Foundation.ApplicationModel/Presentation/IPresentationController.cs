using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Omniscient.Foundation;
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
        /// Begins editing an entity.  The entity will be cloned to preserve original values, in case that <see cref="CancelEdit"/> would be called.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="view">The view that wants to edit the entity.</param>
        /// <param name="entity">The entity to edit</param>
        /// <exception cref="InvalidOperationException">Thrown when the entity is already being edited elsewhere.</exception>
        void BeginEdit<TEntity>(IView view, TEntity entity) where TEntity: IEntity;

        /// <summary>
        /// Ends editing an entity - that is, accept changes.  The clone will be destroyed, and the entity will be permanently changed,
        /// but not saved yet.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="view">The view that was editing the entity.</param>
        /// <param name="entity">The entity that was edited.</param>
        void EndEdit<TEntity>(IView view, TEntity entity) where TEntity : IEntity;

        /// <summary>
        /// Cancels editing the entity.  The entity will be recopied against the clone, and the status set back to <see cref="EntityStatus.Clean"/>.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="view">The view that was editing the entity.</param>
        /// <param name="entity">The entity that was edited.</param>
        void CancelEdit<TEntity>(IView view, TEntity entity) where TEntity : IEntity;

        /// <summary>
        /// Gets the list of view controllers.  Default use is to feed that list at application startup.
        /// </summary>
        List<IViewController> ViewControllers { get; }
    }
}
