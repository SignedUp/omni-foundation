namespace Omniscient.Foundation.Data
{
    ///<summary>
    /// Represents a class that observes modifications in an entity,
    /// and gets notified when any changes occur.
    ///</summary>
    public interface IEntityModificationObserver<TEntity>
    {
        ///<summary>
        /// Server to inform the observer of an entity modification.
        ///</summary>
        ///<param name="entity">Entity that has been modified.</param>
        void Notify(TEntity entity);
    }
}
