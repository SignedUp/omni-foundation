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

        void OpenView(IModel model);

        void ViewClosed(IView view);

        void BeginEdit<TEntity>(IView view, TEntity entity) where TEntity: IEntity, new();

        void EndEdit<TEntity>(IView view, TEntity entity) where TEntity: IEntity, new();

        void CancelEdit<TEntity>(IView view, TEntity entity) where TEntity: IEntity, new();

        List<IViewController> ViewControllers { get; }

        IEntityControllerStore EntityControllerStore { get; set; }
    }
}
