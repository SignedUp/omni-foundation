using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Omniscient.Foundation.Data;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    public class PresentationController: IPresentationController
    {
        private Dictionary<Guid, object> _locks;
        private object _lock;

        public PresentationController()
        {
            _locks = new Dictionary<Guid, object>();
            _lock = new object();
        }

        public IModel CreateModel(string modelName, IEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool LockEntity(Guid entityId)
        {
            lock (_lock)
            {
                if (IsLocked(entityId)) return false;
                _locks.Add(entityId, null);
                return true;
            }                
        }

        public bool IsLocked(Guid entityId)
        {
            return _locks.ContainsKey(entityId);
        }

        public List<IViewController> ViewControllers
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void OpenView(IModel model)
        {
            throw new NotImplementedException();
        }
    }
}
