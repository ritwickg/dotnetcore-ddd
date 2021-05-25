using InventoryManagement.Domain.Contracts;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace InventoryManagement.Infrastructure.Services
{
    public class UserService : RepositoryService<User>, IUserService
    {
        public UserService(InventoryDbContext dbContext) : base(dbContext)
        {
        }
        public override IQueryable<User> GetAll()
        {
            return base.GetAll().Include(x => x.Membership);
        }
        public List<User> GetAllUsers()
        {
            return GetAll().Include(x => x.Membership).ToList();
        }

        public User GetUserById(Guid Id)
        {
            throw new NotImplementedException();
        }
        public override IQueryable<User> GetByCriteria(Expression<Func<User, bool>> exp)
        {
            return base.GetByCriteria(exp).Include(x => x.Membership);
        }
        public override User GetById(Guid Id)
        {
            return this.GetAll().FirstOrDefault(x => x.Id == Id);
        }
    }
}
