using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Sandogh.Domain.Common
{
    public interface IRepository<TEntity> where TEntity : Entity, new()
    {
        void Insert(TEntity entity);
        void Remove(TEntity entity);
        void Update(TEntity entity);
        int SaveChanges();
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        bool Exists(Expression<Func<TEntity, bool>> expression);
    }
}
