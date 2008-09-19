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

        bool LockEntity(Guid entityId);

        bool IsLocked(Guid entityId);

        List<IViewController> ViewControllers { get; set; }

        void OpenView(IModel model);
    }
}
