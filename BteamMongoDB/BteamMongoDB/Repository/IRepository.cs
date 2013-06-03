using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Bson;

namespace BteamMongoDB.Repository
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TId">The type of the id.</typeparam>
    public interface IRepository<TEntity, TId> : IDisposable where TEntity : IMongoEntity<TId>
        where TId : new()
    {
        /// <summary>
        /// Gets the current context connection
        /// </summary>
        IContext GetContext();

        /// <summary>
        /// Creates the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        void Create(TEntity instance);

        /// <summary>
        /// Saves the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        void Save(TEntity instance);

        /// <summary>
        /// Finds the by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        TEntity FindById(TId id);

        /// <summary>
        /// Finds the one.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        TEntity FindOne(object spec);

        /// <summary>
        /// Finds the one.
        /// </summary>
        /// <param name="func">The func.</param>
        /// <returns></returns>
        TEntity FindOne(Expression<Func<TEntity, bool>> func);

        /// <summary>
        /// Finds the specified spec.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        IEnumerable<TEntity> Find(object spec);

        /// <summary>
        /// Finds the specified func.
        /// </summary>
        /// <param name="func">The func.</param>
        /// <returns></returns>
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> func);

        /// <summary>
        /// Removes the specified spec.
        /// </summary>
        /// <param name="spec">The spec.</param>
        void Remove(object spec);

        /// <summary>
        /// Removes the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        void Remove(TEntity instance);

        /// <summary>
        /// Removes the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        void Remove(TId id);

        /// <summary>
        /// Finds the specified spec.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="skip">The skip.</param>
        /// <returns></returns>
        IEnumerable<TEntity> Find(object spec, int limit, int skip);

        /// <summary>
        /// Finds the specified spec.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <param name="orderby">The orderby.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="skip">The skip.</param>
        /// <returns></returns>
        IEnumerable<TEntity> Find(object spec, object orderby, int limit, int skip);

        /// <summary>
        /// Counts the specified spec.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        long Count(object spec);

        /// <summary>
        /// Updates the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="action">The action.</param>
        /// <param name="createIfNew"> </param>
        void Update(TId id, Action<IModifierExpression<TEntity, TId>> action, bool createIfNew = false);

        /// <summary>
        /// Counts the specified func.
        /// </summary>
        /// <param name="func">The func.</param>
        /// <returns></returns>
        long Count(Expression<Func<TEntity, bool>> func);

        /// <summary>
        /// Updates the specified id.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <param name="action">The action.</param>
        void UpdateAll(object spec, Action<IModifierExpression<TEntity, TId>> action);

        /// <summary>
        /// Finds the specified func.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <param name="action">The action.</param>
        /// <param name="createIfNew"> </param>
        /// <param name="getUpdatedEntity"> </param>
        TEntity FindAndModify(object spec, Action<IModifierExpression<TEntity, TId>> action, bool createIfNew = false, bool getUpdatedEntity = false);

        /// <summary>
        /// Finds the specified func.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="action">The action.</param>
        /// <param name="createIfNew">if set to <c>true</c> [create if new].</param>
        /// <param name="getUpdatedEntity">if set to <c>true</c> [get updated entity].</param>
        TEntity FindByIdAndModify(TId id, Action<IModifierExpression<TEntity, TId>> action, bool createIfNew = false, bool getUpdatedEntity = false);

        /// <summary>
        /// Updates the specified id.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <param name="action">The action.</param>
        /// <param name="createIfNew">if set to <c>true</c> [create if new].</param>
        void Update(object spec, Action<IModifierExpression<TEntity, TId>> action, bool createIfNew = false);

        /// <summary>
        /// Aggregates the specified operations.
        /// </summary>
        /// <param name="operations">The operations.</param>
        /// <returns></returns>
        IEnumerable<TEntity> Aggregate(IEnumerable<BsonDocument> operations);

        /// <summary>
        /// Aggregates the specified operations.
        /// </summary>
        /// <param name="operations">The operations.</param>
        /// <returns></returns>
        IEnumerable<TEntity> Aggregate(BsonDocument[] operations);
    }
}