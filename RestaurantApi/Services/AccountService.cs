using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestaurantApi.Entities;
using RestaurantApi.Exceptions;
using RestaurantApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApi.Services
{
    public interface IAccountService
    {
        void CreateUser(CreateUserModel model);
        string GenereateToken(LoginModel model);
    }
    public class AccountService : IAccountService
    {
        private readonly DBContext context;
        private readonly IPasswordHasher<User> passwordHasher;
        private readonly Authentication auth;

        public AccountService(DBContext context, IPasswordHasher<User> passwordHasher, Authentication auth)
        {
            this.context = context;
            this.passwordHasher = passwordHasher;
            this.auth = auth;
        }
        public void CreateUser(CreateUserModel model)
        {
            var newUser = new User()
            {
                Email = model.Email,
                BirthDate = model.BirthDate,
                Nationality = model.Nationality,
                RoleId = model.RoleId != 0 ? model.RoleId : 1
            };

            var password = passwordHasher.HashPassword(newUser, model.Password);
            newUser.PasswordHash = password;

            context.Users.Add(newUser);

            context.SaveChanges();
        }

        public string GenereateToken(LoginModel model)
        {
            var user = context.Users
                .Include(x => x.Role)
                .FirstOrDefault(x => x.Email == model.Email);

            if(user is null)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);

            if(result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, user.Role.Name),
                new Claim("DateOfBirth", user.BirthDate.Value.ToString("yyyy-MM-dd")),
                new Claim("Nationality", user.Nationality)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(auth.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(auth.JwtExpire);

            var token = new JwtSecurityToken(
                auth.JwtIssuer, auth.JwtIssuer, 
                claims, expires : expires, signingCredentials: cred);

            var tokenHander = new JwtSecurityTokenHandler();

            return tokenHander.WriteToken(token);
        }
    }
}
