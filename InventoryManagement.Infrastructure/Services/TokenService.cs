using InventoryManagement.Domain.Contracts;
using InventoryManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.Services
{
    public class TokenService : ITokenProvider
    {
        private readonly UserManager<User> _userManager;
        private readonly Configsettings _configSettings;
        public TokenService(UserManager<User> _userManager, IOptions<Configsettings> options)
        {
            this._userManager = _userManager;
            this._configSettings = options.Value;
        }
        public async Task<string> GenerateJwt(User user)
        {
            IList<string> userRolesList = await this._userManager.GetRolesAsync(user);
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role,userRolesList[0]),
                new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString())
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configSettings.Jwt.JwtKey));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            DateTime expires = DateTime.UtcNow.AddMinutes(60);

            JwtSecurityToken token = new JwtSecurityToken(
             _configSettings.Jwt.JwtIssuer,
             _configSettings.Jwt.JwtAudience,
             claims,
             expires: expires,
             signingCredentials: credentials
             );

            string jwtSecurityToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtSecurityToken;
        }
    }
}
