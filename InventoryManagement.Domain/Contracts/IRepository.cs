using System;
using System.Linq;
using System.Linq.Expressions;

namespace InventoryManagement.Domain.Contracts
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        T GetById(Guid Id);
        IQueryable<T> GetByCriteria(Expression<Func<T, bool>> exp);
        int Add(T data);
        int Remove(T data);
        int Update(T data);
    }
}
