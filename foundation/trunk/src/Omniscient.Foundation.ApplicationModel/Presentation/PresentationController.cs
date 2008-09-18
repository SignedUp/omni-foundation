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

        public bool LockEntity(IEntity entity)
        {
            lock (_lock)
            {
                if (IsLocked(entity)) return false;
                _locks.Add(entity.Id, null);
                return true;
            }                
        }

        public bool IsLocked(IEntity entity)
        {
            return _locks.ContainsKey(entity.Id);
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
