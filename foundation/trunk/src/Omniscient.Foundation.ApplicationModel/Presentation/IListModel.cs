using Omniscient.Foundation.Data;
using Omniscient.Foundation.Patterns;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    public interface IListModel<T> : IModel, IListWrapper<T> { }
}
