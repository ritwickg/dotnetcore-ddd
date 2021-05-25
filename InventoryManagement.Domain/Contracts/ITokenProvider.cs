using InventoryManagement.Domain.Entities;
using System.Threading.Tasks;

namespace InventoryManagement.Domain.Contracts
{
    public interface ITokenProvider
    {
        Task<string> GenerateJwt(User user);
    }
}
