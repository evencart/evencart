using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DotEntity;
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

        public virtual void Insert(T entity)
        {
            EntitySet<T>.Insert(entity);
            //publish the event so they can be handled
            _eventPublisherService.Publish(entity, EventType.Insert);
        }

        public void Insert(T[] entity)
        {
            EntitySet<T>.Insert(entity);
        }

        public virtual void Delete(T entity)
        {
            var deletable = entity as ISoftDeletable;
            if (deletable != null)
            {
                var entityAsSoftDeletable = deletable;
                entityAsSoftDeletable.Deleted = true;
                Update(entity);
            }
            else
            {
                //publish the event so they can be handled
                _eventPublisherService.Publish(entity, EventType.Delete);
                EntitySet<T>.Delete(entity);
            }
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            EntitySet<T>.Delete(where);
        }

        public virtual void Update(T entity)
        {
            EntitySet<T>.Update(entity);

            //publish the event so they can be handled
            _eventPublisherService.Publish(entity, EventType.Update);
        }

        public virtual T Get(int id)
        {
            var cacheKey = $"GET_{typeof(T).Name}_{id}";
            return _cacheProvider.Get(cacheKey, () =>
            {
                return FirstOrDefault(x => x.Id == id);
            });
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> @where)
        {
            return EntitySet<T>.Where(where).Select();
        }

        public T FirstOrDefault(Expression<Func<T, bool>> @where)
        {
            return EntitySet<T>.Where(where).SelectSingle();
        }

        public IEnumerable<T> Query(string query, object parameters = null)
        {
            return EntitySet<T>.Query(query, parameters);
        }
    }
}