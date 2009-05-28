using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    public class ListViewModelBase<TModel>: IListViewModel<TModel>
        where TModel: IModel
    {
        #region IObjectWrapper<List<TModel>> Members

        public IList<TModel> Adapt()
        {
            return this;
        }

        public void Wrap(IList<TModel> item)
        {
            if (Original != null) throw new InvalidOperationException("Models already wrapped.");

            Original = item;
            foreach (TModel model in item)
                this.Add(model);
        }

        #endregion

        protected internal IList<TModel> Original { get; set; }

        #region IList<TModel> Members

        public virtual int IndexOf(TModel item)
        {
            return Original.IndexOf(item);
        }

        public virtual void Insert(int index, TModel item)
        {
            Original.Insert(index, item);
        }

        public virtual void RemoveAt(int index)
        {
            Original.RemoveAt(index);
        }

        public virtual TModel this[int index]
        {
            get
            {
                return Original[index];
            }
            set
            {
                Original[index] = value;
            }
        }

        #endregion

        #region ICollection<TModel> Members

        public virtual void Add(TModel item)
        {
            Original.Add(item);
        }

        public virtual void Clear()
        {
            Original.Clear();
        }

        public virtual bool Contains(TModel item)
        {
            return Original.Contains(item);
        }

        public virtual void CopyTo(TModel[] array, int arrayIndex)
        {
            Original.CopyTo(array, arrayIndex);
        }

        public virtual int Count
        {
            get { return Original.Count; }
        }

        public virtual bool IsReadOnly
        {
            get { return Original.IsReadOnly; }
        }

        public virtual bool Remove(TModel item)
        {
            return Original.Remove(item);
        }

        #endregion

        #region IEnumerable<TModel> Members

        public virtual IEnumerator<TModel> GetEnumerator()
        {
            foreach (TModel model in Original) yield return model;
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}
