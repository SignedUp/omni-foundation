using Omniscient.Foundation.Patterns;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    public interface IViewModel : IObjectWrapper<IModel> { }

    public interface IViewModel<TModel> : IObjectWrapper<TModel>
        where TModel : IModel { }
}
