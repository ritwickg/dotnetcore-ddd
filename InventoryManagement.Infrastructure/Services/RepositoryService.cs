using InventoryManagement.Domain.Contracts;
using InventoryManagement.Infrastructure.Data;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace InventoryManagement.Infrastructure.Services
{
    public class RepositoryService<T> : IRepository<T> where T: class, new()
    {
        private readonly InventoryDbContext dbContext;
        public RepositoryService(InventoryDbContext context)
        {
            this.dbContext = context;
        }
        public int Add(T data)
        {
            dbContext.Set<T>().Add(data);
            return dbContext.SaveChanges();
        }

        public virtual IQueryable<T> GetAll()
        {
            return dbContext.Set<T>();    
        }

        public virtual IQueryable<T> GetByCriteria(Expression<Func<T, bool>> exp)
        {
            return dbContext.Set<T>().Where(exp);
        }

        public virtual T GetById(Guid Id)
        {
            return dbContext.Set<T>().Find(Id);
        }

        public int Remove(T data)
        {
            dbContext.Set<T>().Remove(data);
            return dbContext.SaveChanges();
        }

        public int Update(T data)
        {
            dbContext.Set<T>().Update(data);
            return dbContext.SaveChanges();
        }
    }
}
