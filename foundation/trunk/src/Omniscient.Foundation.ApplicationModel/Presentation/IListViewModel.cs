using Omniscient.Foundation.Patterns;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    public interface IListViewModel : IListWrapper<IModel> { }

    public interface IListViewModel<TModel> : IListWrapper<TModel>
        where TModel : IModel { }
}
