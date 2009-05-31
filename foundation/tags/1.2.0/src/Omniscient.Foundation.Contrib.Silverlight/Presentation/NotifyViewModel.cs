using System;
using System.ComponentModel;
using Omniscient.Foundation.ApplicationModel.Presentation;

namespace Omniscient.Foundation.Contrib.Silverlight.Presentation
{
    public class NotifyViewModel<TModel>: IViewModel, INotifyPropertyChanged
        where TModel: IModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public IModel Adapt()
        {
            return default(TModel);
        }

        public void Wrap(IModel item)
        {
            if (Original != null) throw new InvalidOperationException("Model already wrapped.");
            Original = item;
        }

        protected IModel Original { get; set; }

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
