using InventoryManagement.Domain.Contracts;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace InventoryManagement.Infrastructure.Services
{
    public class ProductService : RepositoryService<Product>, IProductService
    {
        public ProductService( InventoryDbContext dbContext) : base(dbContext)
        {
        }
        public override IQueryable<Product> GetAll()
        {
            return base.GetAll().Include(x => x.Category).Include(x => x.Brand).Include(x => x.User).Include(x => x.Currency)
                .Include(x => x.ProductImages).Include(x => x.ProductStocks);
        }
    }
}
