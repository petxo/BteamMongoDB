using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Wrappers;

namespace BteamMongoDB.Repository
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TId">The type of the id.</typeparam>
    public class Repository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : AbstractMongoEntity<TId>
        where TId : new()
    {
        protected readonly IMongoHelper _mongoHelper;
        protected readonly string _collection;


        /// <summary>
        /// Initializes a new instance of the <see cref="Repository&lt;TEntity, TId&gt;"/> class.
        /// </summary>
        /// <param name="connectionName">Name of the connection.</param>
        public Repository(string connectionName)
            : this(connectionName, typeof(TEntity).Name, new List<MongoIndex>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository&lt;TEntity, TId&gt;"/> class.
        /// </summary>
        /// <param name="connectionName">Name of the connection.</param>
        /// <param name="collection">The collection.</param>
        public Repository(string connectionName, string collection)
            : this(connectionName, collection, new List<MongoIndex>())
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity, TId}"/> class.
        /// </summary>
        /// <param name="connectionName">Name of the connection.</param>
        /// <param name="collection">The collection.</param>
        /// <param name="indexes">The indexes.</param>
        public Repository(string connectionName, string collection, IEnumerable<MongoIndex> indexes)
        {
            _mongoHelper = MongoHelperProvider.Instance.GetHelper(connectionName);
            _collection = collection;

            if (indexes == null || !indexes.Any())
                return;

            var mongoCollection = _mongoHelper.Repository.GetCollection<TEntity>(_collection);
            foreach (MongoIndex index in indexes)
            {
                mongoCollection.EnsureIndex(index.GetKey(), IndexOptions.SetName(index.Name));
            }
        }

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <returns></returns>
        public IContext GetContext()
        {
            _mongoHelper.Generate();
            return _mongoHelper;
        }

        /// <summary>
        /// Creates the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Create(TEntity instance)
        {
            _mongoHelper.Repository.GetCollection<TEntity>(_collection).Insert(instance);
        }

        /// <summary>
        /// Saves the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Save(TEntity instance)
        {
            _mongoHelper.Repository.GetCollection<TEntity>(_collection).Save(instance);
        }

        /// <summary>
        /// Updates the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="action">The action.</param>
        /// <param name="createIfNew"> </param>
        public void Update(TId id, Action<IModifierExpression<TEntity, TId>> action, bool createIfNew = false)
        {
            Update(new { _id = id }, action, createIfNew);
        }

        /// <summary>
        /// Updates the specified id.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <param name="action">The action.</param>
        /// <param name="createIfNew">if set to <c>true</c> [create if new].</param>
        public void Update(object spec, Action<IModifierExpression<TEntity, TId>> action, bool createIfNew = false)
        {
            var modifier = new ModifierExpression<TEntity, TId>();
            action(modifier);

            _mongoHelper.Repository.GetCollection<TEntity>(_collection)
                .Update(QueryWrapper.Create(spec), modifier.GetUpdateBuilder(), createIfNew ? UpdateFlags.Upsert : UpdateFlags.None);
        }

        /// <summary>
        /// Updates the specified id.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <param name="action">The action.</param>
        public void UpdateAll(object spec, Action<IModifierExpression<TEntity, TId>> action)
        {
            var modifier = new ModifierExpression<TEntity, TId>();
            action(modifier);

            _mongoHelper.Repository.GetCollection<TEntity>(_collection)
                .Update(QueryWrapper.Create(spec), modifier.GetUpdateBuilder(), UpdateFlags.Multi);
        }

        /// <summary>
        /// Finds the by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public TEntity FindById(TId id)
        {
            using (_mongoHelper.Repository.RequestStart())
            {

                return
                    _mongoHelper.Repository.GetCollection<TEntity>(_collection).FindOne(
                        QueryWrapper.Create(new {_id = id}));
            }
        }

        /// <summary>
        /// Finds the one.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        public TEntity FindOne(object spec)
        {
            using (_mongoHelper.Repository.RequestStart())
            {

                return _mongoHelper.Repository.GetCollection<TEntity>(_collection).FindOne(QueryWrapper.Create(spec));
            }
        }

        /// <summary>
        /// Finds the one.
        /// </summary>
        /// <param name="func">The func.</param>
        /// <returns></returns>
        public virtual TEntity FindOne(Expression<Func<TEntity, bool>> func)
        {
            using (_mongoHelper.Repository.RequestStart())
            {

                return
                    _mongoHelper.Repository.GetCollection<TEntity>(_collection).AsQueryable().Where(func).FirstOrDefault();
            }
        }

        /// <summary>
        /// Finds the specified spec.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        public IEnumerable<TEntity> Find(object spec)
        {
            using (_mongoHelper.Repository.RequestStart())
            {

                return _mongoHelper.Repository.GetCollection<TEntity>(_collection).Find(QueryWrapper.Create(spec)).ToList();
            }
        }

        /// <summary>
        /// Aggregates the specified operations.
        /// </summary>
        /// <param name="operations">The operations.</param>
        /// <returns></returns>
        public IEnumerable<TEntity> Aggregate(IEnumerable<BsonDocument> operations)
        {
            var resultado = _mongoHelper.Repository.GetCollection(_collection).Aggregate(operations);
            
            return resultado.Ok ? resultado.ResultDocuments.Select(BsonSerializer.Deserialize<TEntity>).ToList() 
                : new List<TEntity>();
        }

        /// <summary>
        /// Aggregates the specified operations.
        /// </summary>
        /// <param name="operations">The operations.</param>
        /// <returns></returns>
        public IEnumerable<TEntity> Aggregate(BsonDocument[] operations)
        {
            return Aggregate(operations.ToList());
        }

        /// <summary>
        /// Finds the count.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        public long Count(object spec)
        {
            return _mongoHelper.Repository.GetCollection<TEntity>(_collection).Count(QueryWrapper.Create(spec));
        }

        /// <summary>
        /// Counts the specified func.
        /// </summary>
        /// <param name="func">The func.</param>
        /// <returns></returns>
        public long Count(Expression<Func<TEntity, bool>> func)
        {
            return _mongoHelper.Repository.GetCollection<TEntity>(_collection).AsQueryable().Count(func);
        }

        /// <summary>
        /// Finds the specified spec.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="skip">The skip.</param>
        /// <returns></returns>
        public IEnumerable<TEntity> Find(object spec, int limit, int skip)
        {
            using (_mongoHelper.Repository.RequestStart())
            {
                return _mongoHelper.Repository.GetCollection<TEntity>(_collection).Find(QueryWrapper.Create(spec))
                    .SetSkip(skip)
                    .SetLimit(limit)
                    .ToList();
            }
        }

        /// <summary>
        /// Finds the specified spec.
        /// </summary>
        /// <param name="func">The func.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="skip">The skip.</param>
        /// <returns></returns>
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> func, int limit, int skip)
        {
            using (_mongoHelper.Repository.RequestStart())
            {
                return _mongoHelper.Repository.GetCollection<TEntity>(_collection).AsQueryable().Where(func)
                    .Skip(skip)
                    .Take(limit)
                    .ToList();
            }
        }

        /// <summary>
        /// Finds the specified spec.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <param name="orderby">The orderby.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="skip">The skip.</param>
        /// <returns></returns>
        public IEnumerable<TEntity> Find(object spec, object orderby, int limit, int skip)
        {
            using (_mongoHelper.Repository.RequestStart())
            {

                return _mongoHelper.Repository.GetCollection<TEntity>(_collection).Find(QueryWrapper.Create(spec))
                    .SetSortOrder(SortByWrapper.Create(orderby))
                    .SetSkip(skip)
                    .SetLimit(limit)
                    .ToList();
            }
        }

        /// <summary>
        /// Finds the specified spec.
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="func">The func.</param>
        /// <param name="keySelector">The key selector.</param>
        /// <param name="ascending">if set to <c>true</c> [ascending].</param>
        /// <param name="limit">The limit.</param>
        /// <param name="skip">The skip.</param>
        /// <returns></returns>
        public IEnumerable<TEntity> Find<U>(Expression<Func<TEntity, bool>> func, Expression<Func<TEntity, U>> keySelector, bool ascending, int limit, int skip)
        {
            using (_mongoHelper.Repository.RequestStart())
            {

                var query = _mongoHelper.Repository.GetCollection<TEntity>(_collection).AsQueryable().Where(func);

                query = ascending ? query.OrderBy(keySelector) : query.OrderByDescending(keySelector);

                return query
                    .Skip(skip)
                    .Take(limit)
                    .ToList();
            }
        }

        /// <summary>
        /// Finds the specified func.
        /// </summary>
        /// <param name="func">The func.</param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> func)
        {
            using (_mongoHelper.Repository.RequestStart())
            {
                return _mongoHelper.Repository.GetCollection<TEntity>(_collection).AsQueryable().Where(func).ToList();
            }
        }

        /// <summary>
        /// Finds the specified func.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <param name="action">The action.</param>
        /// <param name="createIfNew"> </param>
        /// <param name="getUpdatedEntity"> </param>
        public virtual TEntity FindAndModify(object spec, Action<IModifierExpression<TEntity, TId>> action, bool createIfNew = false, bool getUpdatedEntity = false)
        {
            var modifier = new ModifierExpression<TEntity, TId>();
            action(modifier);

            return _mongoHelper.Repository.GetCollection<TEntity>(_collection)
                                   .FindAndModify(QueryWrapper.Create(spec),
                                                    SortBy.Null,
                                                    modifier.GetUpdateBuilder(), getUpdatedEntity, createIfNew)
                                   .GetModifiedDocumentAs<TEntity>();
        }

        /// <summary>
        /// Finds the specified func.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="action">The action.</param>
        /// <param name="createIfNew">if set to <c>true</c> [create if new].</param>
        /// <param name="getUpdatedEntity">if set to <c>true</c> [get updated entity].</param>
        public virtual TEntity FindByIdAndModify(TId id, Action<IModifierExpression<TEntity, TId>> action, bool createIfNew = false, bool getUpdatedEntity = false)
        {
            return FindAndModify(new {_id = id}, action, createIfNew, getUpdatedEntity);
        }


        /// <summary>
        /// Removes the specified spec.
        /// </summary>
        /// <param name="spec">The spec.</param>
        public void Remove(object spec)
        {
            _mongoHelper.Repository.GetCollection<TEntity>(_collection).Remove(QueryWrapper.Create(spec));
        }

        /// <summary>
        /// Removes the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Remove(TEntity instance)
        {
            Remove(instance.Id);
        }

        /// <summary>
        /// Removes the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        public void Remove(TId id)
        {
            _mongoHelper.Repository.GetCollection<TEntity>(_collection)
                .FindAndRemove(QueryWrapper.Create(new { _id = id }), SortBy.Null);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
               
            }
        }
    }
}