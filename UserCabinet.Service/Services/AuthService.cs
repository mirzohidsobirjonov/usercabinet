using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserCabinet.Data.IRepositories;
using UserCabinet.Service.Exceptions.Users;
using UserCabinet.Service.Helpers;
using UserCabinet.Service.Interfaces;

namespace UserCabinet.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IConfiguration configuration;
        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
        }

        public async Task<string> GenerateTokenAsync(string username, string password)
        {
            var user = await unitOfWork.Users.GetAsync(x =>
            x.Username == username && x.Password == password.Encode());
            if (user is null)
                throw new UserException(400, "Login or password is incorrect");

            // Else we generate JSON Web Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim("Id", user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(int.Parse(configuration["JWT:ExpTime"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
