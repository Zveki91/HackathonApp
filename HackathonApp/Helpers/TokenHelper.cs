using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HackathonApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace HackathonApp.Helpers
{
    public class TokenHelper
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public TokenHelper(IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }
        
        public async Task<object> GenerateJwtToken(ApplicationUser user, string role)
        {
            var claims =await  _userManager.GetClaimsAsync(user);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}