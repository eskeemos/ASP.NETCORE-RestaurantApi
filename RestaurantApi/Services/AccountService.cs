using Microsoft.AspNetCore.Identity;
using RestaurantApi.Entities;
using RestaurantApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApi.Services
{
    public interface IAccountService
    {
        void CreateUser(CreateUserModel model);
    }
    public class AccountService : IAccountService
    {
        private readonly DBContext context;
        private readonly IPasswordHasher<User> passwordHasher;

        public AccountService(DBContext context, IPasswordHasher<User> passwordHasher)
        {
            this.context = context;
            this.passwordHasher = passwordHasher;
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
    }
}
