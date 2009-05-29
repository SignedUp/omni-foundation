using System;
using System.ComponentModel;
using Omniscient.Foundation.ApplicationModel.Presentation;

namespace Omniscient.Foundation.Contrib.Silverlight.Presentation
{
    public class NotifyViewModel<TModel>: IViewModel<TModel>, INotifyPropertyChanged
        where TModel: IModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public TModel Adapt()
        {
            return default(TModel);
        }

        public void Wrap(TModel item)
        {
            if (Original != null) throw new InvalidOperationException("Model already wrapped.");
            Original = item;
        }

        protected TModel Original { get; set; }

        protected virtual void OnValueChanged(string propertyName)
        {
            RaisePropertyChanged(propertyName);
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }
    }
}
