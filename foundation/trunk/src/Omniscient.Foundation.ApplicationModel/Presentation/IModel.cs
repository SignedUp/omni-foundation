using System;
using Omniscient.Foundation.Data;
using System.ComponentModel;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    /// <summary>
    /// Represents a model.  Models are logical wrappers around entities; they define relations between entities
    /// and are responsible for managing the state and status of entities.
    /// </summary>
    public interface IModel: INotifyPropertyChanged
    {
        IEntity Entity { get; }
        EntityStatus EntityStatus { get; }

        void BeginEdit();
        void EndEdit(bool acceptChanges);
        bool IsBeingEdited { get; }

        void MarkAsDeleted();
    }

    public interface IModel<TEntity> : IModel
        where TEntity : IEntity, new()
    {
        TEntity Entity { get; }
    }
}
