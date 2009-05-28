using System.Collections.Generic;
using System.ComponentModel;
using Omniscient.Foundation.Data;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    public class NotifyEntityModelBase<TEntity>: EntityModelBase<TEntity>, INotifyPropertyChanged
        where TEntity : IEntity, new()
    {
        private List<string> _changedProperties;
    
        public event PropertyChangedEventHandler PropertyChanged;

        public NotifyEntityModelBase()
            : base() { }

        public NotifyEntityModelBase(TEntity entity)
            : base(entity) { }

        public override void BeginEdit()
        {
            base.BeginEdit();
            _changedProperties = new List<string>();
        }

        public override void EndEdit(bool acceptChanges)
        {
            base.EndEdit(acceptChanges);
            if (acceptChanges)
                _changedProperties.ForEach(propertyName => RaisePropertyChanged(propertyName));
        }

        protected virtual void OnValueChanged(string propertyName)
        {
            if (IsBeingEdited)
            {
                _changedProperties.Add(propertyName);
            }
            else
            {
                RaisePropertyChanged(propertyName);
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }
    }
}
