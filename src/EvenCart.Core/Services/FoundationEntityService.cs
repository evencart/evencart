#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DotEntity;
using DotEntity.Enumerations;
using DryIoc;
using EvenCart.Core.Caching;
using EvenCart.Core.Data;
using EvenCart.Core.Infrastructure;
using EvenCart.Core.Services.Events;

namespace EvenCart.Core.Services
{
    public abstract class FoundationEntityService<T> : IFoundationEntityService<T> where T : FoundationEntity
    {
        private readonly IEventPublisherService _eventPublisherService;
        protected readonly ICacheProvider CacheProvider;

        protected IEntitySet<T> Repository => RepositoryExplorer<T>();

        protected static IEntitySet<T1> RepositoryExplorer<T1>() where T1 : FoundationEntity
        {
            return EntitySet<T1>.Just();
        }

        protected FoundationEntityService()
        {
            CacheProvider = CacheProviders.PrimaryProvider;
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
                if (transaction == null)
                    EntitySet<T>.Delete(entity);
                else
                {
                    EntitySet<T>.Delete(entity, transaction.Value);
                }

                var cacheKey = $"GET_{typeof(T).Name}_{entity.Id}";
                //clear cache
                CacheProvider.Remove(cacheKey);
            }
            //publish the event so they can be handled
            _eventPublisherService.Publish(entity, EventType.Delete);
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
            return CacheProvider.Get(cacheKey, () =>
            {
                return FirstOrDefault(x => x.Id == id);
            });
        }

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> @where, int page = 1, int count = int.MaxValue)
        {
            var query = EntitySet<T>.Where(where).OrderBy(x => x.Id);
            query = _eventPublisherService.Filter(query);
            if (typeof(T).IsAssignableTo(typeof(IStoreEntity)))
                return query.SelectNested(page, count);
            return query.Select(page, count);
        }

        public static IEnumerable<T> Get(Expression<Func<T, bool>> @where)
        {
            return EntitySet<T>.Where(where).Select();
        }

        public virtual IEnumerable<T> Get(out int totalResults, Expression<Func<T, bool>> @where, Expression<Func<T, object>> orderBy = null, RowOrder rowOrder = RowOrder.Ascending, int page = 1, int count = Int32.MaxValue)
        {
            if (orderBy == null)
                orderBy = x => x.Id;
            var query = EntitySet<T>.Where(where).OrderBy(orderBy, rowOrder);
            query = _eventPublisherService.Filter(query);
            if (typeof(T).IsAssignableTo(typeof(IStoreEntity)))
                return query.SelectNestedWithTotalMatches(out totalResults, page, count);
            return query.SelectWithTotalMatches(out totalResults, page, count);
        }

        public virtual T FirstOrDefault(Expression<Func<T, bool>> @where)
        {
            var query = EntitySet<T>.Where(where);
            query = _eventPublisherService.Filter(query);
            if (typeof(T).IsAssignableTo(typeof(IStoreEntity)))
                return query.SelectNested().FirstOrDefault();
            return query.SelectSingle();
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