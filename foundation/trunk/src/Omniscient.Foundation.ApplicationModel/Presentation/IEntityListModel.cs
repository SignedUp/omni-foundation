using Omniscient.Foundation.Data;
using Omniscient.Foundation.ApplicationModel;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    public interface IEntityListModel : IModel, IListWrapper<IEntity> { }

    public interface IEntityListModel<TEntity> : IModel, IListWrapper<TEntity>
        where TEntity : IEntity, new() { }
}