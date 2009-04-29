namespace Omniscient.Foundation.Data
{
    public interface IEntityComposer<TEntity> where TEntity: IEntity
    {
        IEntityList<TEntity> Compose();
    }
}
