using System;
using System.Linq.Expressions;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace BteamMongoDB.Repository
{
    internal class ModifierExpression<TEntity, TId> : IModifierExpression<TEntity, TId> where TEntity : IMongoEntity<TId>
    {
        private readonly UpdateBuilder updateBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModifierExpression&lt;TEntity&gt;"/> class.
        /// </summary>
        public ModifierExpression()
        {
            updateBuilder = new UpdateBuilder();
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <typeparam name="X"></typeparam>
        /// <param name="func">The func.</param>
        /// <param name="value">The value.</param>
        public void SetValue<X>(Expression<Func<TEntity, object>> func, X value)
        {
            var propertyName = ReflectionHelper.FindProperty(func);
            updateBuilder.SetWrapped(propertyName, value);
        }

        /// <summary>
        /// Pushes the specified func.
        /// </summary>
        /// <typeparam name="X"></typeparam>
        /// <param name="func">The func.</param>
        /// <param name="value">The value.</param>
        public void Push<X>(Expression<Func<TEntity, object>> func, X value)
        {
            var propertyName = ReflectionHelper.FindProperty(func);
            updateBuilder.PushWrapped(propertyName, value);
        }

        /// <summary>
        /// Pulls the specified func.
        /// </summary>
        /// <typeparam name="X"></typeparam>
        /// <param name="func">The func.</param>
        /// <param name="value">The value.</param>
        public void Pull<X>(Expression<Func<TEntity, object>> func, X value)
        {
            var propertyName = ReflectionHelper.FindProperty(func);
            updateBuilder.PullWrapped(propertyName, value);
        }

        /// <summary>
        /// Gets the update builder.
        /// </summary>
        /// <returns></returns>
        public IMongoUpdate GetUpdateBuilder()
        {
            return updateBuilder;
        }
    }
}