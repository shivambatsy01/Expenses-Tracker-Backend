using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebServices.API.Models.Domain;

namespace WebServices.API.Repositories.TokenHandlerRepository
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration configuration;
        public TokenHandler(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<string> CreateTokenAsync(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTConfigurations:APIKey"]));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.GivenName, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var token = new JwtSecurityToken(
                configuration["JWTConfigurations:Issuer"],
                configuration["JWTConfigurations:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(25),
                signingCredentials: credential
                );

            return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }

        //public void fun()
        //{
        //    throw new NotImplementedException(); ;
        //}
    }
}
