using System.Collections.Specialized;
using System.ComponentModel;
using Omniscient.Foundation.ApplicationModel.Presentation;
using System;

namespace Omniscient.Foundation.Contrib.Silverlight.Presentation
{
    public class NotifyListViewModel<TModel>: ListViewModelBase<TModel>, INotifyCollectionChanged, INotifyPropertyChanged
        where TModel: IModel
    {
        private bool _busy;

        public event NotifyCollectionChangedEventHandler CollectionChanged = delegate { };
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void CheckReentrancy()
        {
            if (this._busy)
                throw new InvalidOperationException("Cannot change collection at this point.");
        }

        private void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            this._busy = true;
            try
            {
                this.PropertyChanged(this, e);
            }
            finally
            {
                this._busy = false;
            }
        }

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            this._busy = true;
            try
            {
                this.CollectionChanged(this, e);
            }
            finally
            {
                this._busy = false;
            }
        }

        public override void Clear()
        {
            CheckReentrancy();
            base.Clear();
            OnPropertyChanged("Count");
            OnPropertyChanged("Item[]");
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public override void Insert(int index, TModel item)
        {
            CheckReentrancy();
            base.Insert(index, item);
            OnPropertyChanged("Count");
            OnPropertyChanged("Item[]");
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
        }

        public override TModel this[int index]
        {
            get { return base[index]; }
            set 
            {
                this.CheckReentrancy();
                TModel oldItem = base[index];
                base[index] = value;
                this.OnPropertyChanged("Item[]");
                this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value, oldItem, index));
            }
        }

        public override bool Remove(TModel item)
        {
            int index = base.IndexOf(item);
            if (index < 0) return false;
            this.RemoveAt(index);
            return true;
        }

        public override void RemoveAt(int index)
        {
            this.CheckReentrancy();
            TModel changedItem = base[index];
            base.RemoveAt(index);
            this.OnPropertyChanged("Count");
            this.OnPropertyChanged("Item[]");
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, changedItem, index));
        }

    }
}
