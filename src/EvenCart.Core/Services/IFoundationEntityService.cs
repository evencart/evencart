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
using System.Linq.Expressions;
using DotEntity.Enumerations;
using EvenCart.Core.Data;

namespace EvenCart.Core.Services
{
    public interface IFoundationEntityService<T> where T : FoundationEntity
    {
        void Insert(T entity, Transaction transaction = null);

        void Insert(T[] entity);

        void Delete(T entity, Transaction transaction = null);

        void Delete(Expression<Func<T, bool>> where, Transaction transaction = null);

        void Update(T entity, Transaction transaction = null);

        void Update(object entity, Expression<Func<T, bool>> where, Transaction transaction, Func<T, bool> action = null);

        void InsertOrUpdate(T entity, Transaction transaction = null);

        T Get(int id);

        IEnumerable<T> Get(Expression<Func<T, bool>> where, int page = 1, int count = int.MaxValue);

        IEnumerable<T> Get(out int totalResults, Expression<Func<T, bool>> where, Expression<Func<T, object>> orderBy = null, RowOrder rowOrder = RowOrder.Ascending,  int page = 1, int count = int.MaxValue);

        T FirstOrDefault(Expression<Func<T, bool>> where);

        IEnumerable<T> Query(string query, object parameters = null);

        int Count(Expression<Func<T, bool>> where = null);
    }

}