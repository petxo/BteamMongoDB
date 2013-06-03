using System;
using System.Linq.Expressions;
using MongoDB.Driver;

namespace BteamMongoDB.Repository
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TId">The type of the id.</typeparam>
    public interface IModifierExpression<TEntity, TId> where TEntity : AbstractMongoEntity<TId>
    {
        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <typeparam name="X"></typeparam>
        /// <param name="func">The func.</param>
        /// <param name="value">The value.</param>
        void SetValue<X>(Expression<Func<TEntity, object>> func, X value);

        /// <summary>
        /// Pushes the specified func.
        /// </summary>
        /// <typeparam name="X"></typeparam>
        /// <param name="func">The func.</param>
        /// <param name="value">The value.</param>
        void Push<X>(Expression<Func<TEntity, object>> func, X value);

        /// <summary>
        /// Gets the update builder.
        /// </summary>
        /// <returns></returns>
        IMongoUpdate GetUpdateBuilder();

        /// <summary>
        /// Pulls the specified func.
        /// </summary>
        /// <typeparam name="X"></typeparam>
        /// <param name="func">The func.</param>
        /// <param name="value">The value.</param>
        void Pull<X>(Expression<Func<TEntity, object>> func, X value);
    }
}