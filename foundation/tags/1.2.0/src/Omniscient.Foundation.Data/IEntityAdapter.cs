﻿    using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Omniscient.Foundation.Data
{
    
    /// <summary>
    /// Represents an adapter that manages a certain type of entities.  
    /// Responsible for connecting to the database, and executing queries.
    /// </summary>
    /// <remarks>
    /// The interface registers for the ServiceModel aspect; in other words, it can be used as a contract for WCF.
    /// </remarks>
    /// <typeparam name="TEntity">The type of entities to manage.</typeparam>
    [ServiceContract]
    public interface IEntityAdapter<TEntity> where TEntity: IEntity
    {
        ///<summary>
        ///</summary>
        ///<returns></returns>
        [OperationContract]
        IList<TEntity> Load();

        /// <summary>
        /// Saves the entity.  Executes different queries based on the <see cref="EntityStatus"/> of the entity.
        /// </summary>
        /// <param name="entity">The entity to save.</param>
        [OperationContract]
        void Save(TEntity entity);

        ///<summary>
        ///</summary>
        ///<param name="entities"></param>
        [OperationContract(Name = "SaveList")]
        void Save(IEnumerable<TEntity> entities);

    }
}
