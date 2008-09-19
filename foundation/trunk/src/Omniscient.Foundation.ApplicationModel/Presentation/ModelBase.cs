using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Omniscient.Foundation.Data;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    public abstract class ModelBase: IModel
    {
        private readonly string _name;

        protected ModelBase()
        {
            _name = this.GetType().Name;
            PresentationController = Application.Current.PresentationController;
        }

        public IPresentationController PresentationController
        {
            get;
            set;
        }

        public virtual string Name
        {
            get { return _name; }
        }

        public abstract bool HasEntity(Guid id);

        public abstract bool ContainsEntitiesThatNeedToBeSaved();

        public abstract void Save();

        public virtual bool TryBeginEdit(Guid entityId)
        {
            if (!this.HasEntity(entityId)) return false;
            return this.PresentationController.LockEntity(entityId);
        }
    }
}
