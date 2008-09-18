using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Omniscient.Foundation;
using Omniscient.Foundation.Data;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    /// <summary>
    /// Represents an application controller.  Responsible for managing locks on Entities and Models lifetime.
    /// </summary>
    public interface IPresentationController
    {
        /// <summary>
        /// Creates a model for entity <typeparamref name="TEntity"/>
        /// </summary>
        /// <param name="modelName">The name of the model as defined in the metadata.</param>
        /// <param name="entity">The root entity.</param>
        /// <returns>An instance of a Model.</returns>
        IModel CreateModel(string modelName, IEntity entity);

        bool LockEntity(IEntity entity);

        bool IsLocked(IEntity entity);

        List<IViewController> ViewControllers { get; set; }

        void OpenView(IModel model);
    }
}
