using InventoryManagement.API.DTO;
using InventoryManagement.Domain.Contracts;
using InventoryManagement.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InventoryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountManager _accountManager;
        private readonly ITokenProvider _tokenManager;

        public AccountController(IAccountManager accountManager, ITokenProvider tokenProvider)
        {
            _accountManager = accountManager;
            _tokenManager = tokenProvider;
        }
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserDto user)
        {
            User newUser = new User()
            {
                Email = user.EmailAddress,
                UserName = user.EmailAddress,
                PhoneNumber = user.PhoneNumber
            };

            ResponseDto response = await _accountManager.AddUserAsync(newUser, user.Role, user.Password);
            if (!response.IsSuccess)
            {
                return BadRequest(response.ErrorMessage);
            }
            string token = await _tokenManager.GenerateJwt(response.User);
            return StatusCode(201, new { token = token });
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto login)
        {
            ResponseDto response = await _accountManager.SignInUser(login.UserName, login.Password);
            if (!response.IsSuccess)
            {
                return BadRequest(response.ErrorMessage);
            }
            string token = await _tokenManager.GenerateJwt(response.User);
            return Ok(token);
        }
        [HttpGet("CheckToken")]
        [Authorize(Roles = "User")]
        public IActionResult CheckToken()
        {
            return Ok();
        }
        [HttpGet("GetUsers")]
        [AllowAnonymous]
        public IActionResult GetUsers()
        {
            return Ok(_accountManager.FindByCriteria());
        }
    }
}
