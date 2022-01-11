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
        void CreateUser(createUserModel model);
    }
    public class AccountService : IAccountService
    {
        private readonly DBContext context;

        public AccountService(DBContext context)
        {
            this.context = context;
        }
        public void CreateUser(createUserModel model)
        {
            var newUser = new User()
            {
                Email = model.Email,
                BirthDate = model.BirthDate,
                Nationality = model.Nationality,
                RoleId = model.RoleId
            };

            context.Users.Add(newUser);
            context.SaveChanges();
        }
    }
}
