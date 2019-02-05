using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DotEntity;
using DotEntity.Enumerations;
using RoastedMarketplace.Core.Caching;
using RoastedMarketplace.Core.Data;
using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Core.Services.Events;

namespace RoastedMarketplace.Core.Services
{
    public abstract class FoundationEntityService<T> : IFoundationEntityService<T> where T : FoundationEntity
    {
        private readonly IEventPublisherService _eventPublisherService;
        private readonly ICacheProvider _cacheProvider;

        protected IEntitySet<T> Repository => RepositoryExplorer<T>();

        protected static IEntitySet<T1> RepositoryExplorer<T1>() where T1 : FoundationEntity
        {
            return EntitySet<T1>.Just();
        }

        protected FoundationEntityService()
        {
            _cacheProvider = DependencyResolver.Resolve<ICacheProvider>(); ;
            //resolve publisher manually
            _eventPublisherService = DependencyResolver.Resolve<IEventPublisherService>();
        }

        public virtual void Insert(T entity, Transaction transaction = null)
        {
            if (transaction == null)
                EntitySet<T>.Insert(entity);
            else
                EntitySet<T>.Insert(entity, transaction.Value);
            //publish the event so they can be handled
            _eventPublisherService.Publish(entity, EventType.Insert);
        }

        public void Insert(T[] entity)
        {
            EntitySet<T>.Insert(entity);
        }

        public virtual void Delete(T entity, Transaction transaction = null)
        {
            var deletable = entity as ISoftDeletable;
            if (deletable != null)
            {
                var entityAsSoftDeletable = deletable;
                entityAsSoftDeletable.Deleted = true;
                Update(entity, transaction);
            }
            else
            {
                //publish the event so they can be handled
                _eventPublisherService.Publish(entity, EventType.Delete);
                if (transaction == null)
                    EntitySet<T>.Delete(entity);
                else
                {
                    EntitySet<T>.Delete(entity, transaction.Value);
                }
            }
        }

        public virtual void Delete(Expression<Func<T, bool>> where, Transaction transaction = null)
        {
            if (transaction == null)
                EntitySet<T>.Delete(where);
            else
                EntitySet<T>.Delete(where, transaction.Value);
        }

        public virtual void Update(T entity, Transaction transaction = null)
        {
            if (transaction == null)
                EntitySet<T>.Update(entity);
            else
                EntitySet<T>.Update(entity, transaction.Value);

            //publish the event so they can be handled
            _eventPublisherService.Publish(entity, EventType.Update);
        }

        public virtual void Update(object entity, Expression<Func<T, bool>> @where, Transaction transaction, Func<T, bool> action = null)
        {
            if (transaction == null)
                EntitySet<T>.Update(entity, where);
            else
                EntitySet<T>.Update(entity, where, transaction?.Value, action);
        }

        public virtual void InsertOrUpdate(T entity, Transaction transaction = null)
        {
            if (entity.Id > 0)
                Update(entity, transaction);
            else
                Insert(entity, transaction);
        }

        public virtual T Get(int id)
        {
            var cacheKey = $"GET_{typeof(T).Name}_{id}";
            return _cacheProvider.Get(cacheKey, () =>
            {
                return FirstOrDefault(x => x.Id == id);
            });
        }

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> @where, int page = 1, int count = int.MaxValue)
        {
            return EntitySet<T>.Where(where).OrderBy(x => x.Id).Select(page, count);
        }

        public IEnumerable<T> Get(out int totalResults, Expression<Func<T, bool>> @where, Expression<Func<T, object>> orderBy = null, RowOrder rowOrder = RowOrder.Ascending, int page = 1, int count = Int32.MaxValue)
        {
            if (orderBy == null)
                orderBy = x => x.Id;
            return EntitySet<T>.Where(where).OrderBy(orderBy, rowOrder).SelectWithTotalMatches(out totalResults, page, count);
        }

        public virtual T FirstOrDefault(Expression<Func<T, bool>> @where)
        {
            return EntitySet<T>.Where(where).SelectSingle();
        }

        public virtual IEnumerable<T> Query(string query, object parameters = null)
        {
            return EntitySet<T>.Query(query, parameters);
        }

        public virtual int Count(Expression<Func<T, bool>> @where = null)
        {
            return @where == null ? EntitySet<T>.Count() : EntitySet<T>.Where(@where).Count();
        }
    }
}