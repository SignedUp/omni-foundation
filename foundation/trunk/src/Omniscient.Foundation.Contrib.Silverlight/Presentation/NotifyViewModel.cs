using System;
using System.ComponentModel;
using Omniscient.Foundation.ApplicationModel.Presentation;

namespace Omniscient.Foundation.Contrib.Silverlight.Presentation
{
    public class NotifyViewModel<TModel> : IViewModel<TModel>, INotifyPropertyChanged where TModel : class, IModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected TModel Original { get; set; }

        public TModel Adapt()
        {
            return default(TModel);
        }

        public void Wrap(TModel item)
        {
            if (Original != null)
            {
                throw new InvalidOperationException("Model already wrapped.");
            }

            Original = item;
        }

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
