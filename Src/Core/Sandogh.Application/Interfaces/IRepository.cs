using Microsoft.EntityFrameworkCore;
using Sandogh.Application.Common;
using Sandogh.Application.Interfaces.Contexts;
using Sandogh.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Application.Interfaces
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

    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly IDataBaseContext dbContext;


        public EfRepository(IDataBaseContext dataBaseContext)
        {
            dbContext = dataBaseContext;
        }


        public bool Exists(Expression<Func<TEntity, bool>> expression)
        {
            return dbContext.Set<TEntity>().Any(expression);
        }

        public TEntity Get(int id)
        {
            return dbContext.Set<TEntity>().FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbContext.Set<TEntity>().ToList();
        }

        public void Insert(TEntity entity)
        {
            dbContext.Set<TEntity>().Add(entity);
            dbContext.SaveChanges();
        }

        public void Remove(TEntity entity)
        {
            dbContext.Set<TEntity>().Remove(entity);
            dbContext.SaveChanges();
        }

        public int SaveChanges()
        {
            return dbContext.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            dbContext.Set<TEntity>().Update(entity);
            dbContext.SaveChanges();
        }
    }
}
