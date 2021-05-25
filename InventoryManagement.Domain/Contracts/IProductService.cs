using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Domain.Contracts
{
    public interface IProductService: IRepository<Product>
    {
    }
}
