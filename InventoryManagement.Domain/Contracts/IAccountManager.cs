using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Entities.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryManagement.Domain.Contracts
{
    public interface IAccountManager
    {
        Task<ResponseDto> AddUserAsync(User user, Role role, string password);
        Task<ResponseDto> SignInUser(string username, string password);
        List<User> GetAllUsers();
        List<User> FindByCriteria();
    }
}
