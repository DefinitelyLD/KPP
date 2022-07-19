using Messenger.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.Token
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        public TokenService(IConfiguration configuration, UserManager<User> userManager) {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<string> BuildTokenAsync(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Role, roles.Single())
            };

            var lifetime = Convert.ToDouble(_configuration["Jwt:Lifetime"]);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(lifetime), 
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}