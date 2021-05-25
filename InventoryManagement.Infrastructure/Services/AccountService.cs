using InventoryManagement.Domain.Contracts;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Entities.Enumerations;
using InventoryManagement.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.Services
{
    public class AccountService : IAccountManager
    {
        private readonly UserManager<User> _userManager;
        private readonly InventoryDbContext _dbContext;
        private readonly SignInManager<User> _signinManager;
        private readonly IUserService userService;
        public AccountService(UserManager<User> userManager, InventoryDbContext dbContext, SignInManager<User> signInManager, IUserService userService)
        {
            this._userManager = userManager;
            this._dbContext = dbContext;
            this._signinManager = signInManager;
            this.userService = userService;
        }
        public async Task<ResponseDto> AddUserAsync(User user, Role role, string password)
        {
            User existingUser = await _userManager.FindByNameAsync(user.Email);
            if (existingUser != null)
            {
                return new ResponseDto() { IsSuccess = false };
            }
            user.Membership = _dbContext.Memberships.FirstOrDefault(membership =>
            membership.MembershipName == MembershipType.Basic.ToString());

            IdentityResult userCreateResult = await _userManager.CreateAsync(user, password);
            if (!userCreateResult.Succeeded)
            {
                return new ResponseDto { IsSuccess = false,
                    ErrorMessage = "User Creation Failed" + userCreateResult.Errors.Select(x => x.Code + ": " + x.Description)
                    .Aggregate((i, j) => i + "\n" + j)
                };
            }
            IdentityResult roleAssignResult = await _userManager.AddToRoleAsync(user, role.ToString());
            if (!roleAssignResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                return new ResponseDto
                {
                    IsSuccess = false,
                    ErrorMessage = "User Creation Failed" + roleAssignResult.Errors.Select(x => x.Code +": "+ x.Description)
                    .Aggregate((i, j) => i + "\n" + j)
                };
            }
            return new ResponseDto { IsSuccess = true, User = user };
        }

        public async Task<ResponseDto> SignInUser(string username, string password)
        {
            SignInResult result = await _signinManager.PasswordSignInAsync(username, password, false, false);
            if (result.Succeeded)
            {
                User user = await _userManager.FindByNameAsync(username);
                return new ResponseDto() { User = user, IsSuccess = true};
            }
            else
            {
                return new ResponseDto() { IsSuccess = false, ErrorMessage = "Invalid Credentials" };
            }
        }
        public List<User> GetAllUsers()
        {
            return userService.GetAll().ToList();
        }

        public List<User> FindByCriteria()
        {
            return userService.GetByCriteria(x => x.Membership.MembershipName == "BASIC").ToList();
        }
    }
}
