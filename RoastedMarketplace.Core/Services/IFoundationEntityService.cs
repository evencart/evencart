using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using RoastedMarketplace.Core.Data;

namespace RoastedMarketplace.Core.Services
{
    public interface IFoundationEntityService<T> where T : FoundationEntity
    {
        void Insert(T entity);

        void Insert(T[] entity);

        void Delete(T entity);

        void Delete(Expression<Func<T, bool>> where);

        void Update(T entity);

        T Get(int id);

        IEnumerable<T> Get(Expression<Func<T, bool>> where);

        T FirstOrDefault(Expression<Func<T, bool>> where);

        IEnumerable<T> Query(string query, object parameters = null);
    }

}