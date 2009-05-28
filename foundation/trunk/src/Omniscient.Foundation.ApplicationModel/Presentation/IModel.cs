using System;
using Omniscient.Foundation.Data;
using System.ComponentModel;
using Omniscient.Foundation.Patterns;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    /// <summary>
    /// Core contract for a model.  See <see cref="IEntityModel"/> for models that wrap entities.
    /// </summary>
    public interface IModel
    {
        void BeginEdit();
        void EndEdit(bool acceptChanges);
        bool IsBeingEdited { get; }
        void MarkAsDeleted();
    }
}
