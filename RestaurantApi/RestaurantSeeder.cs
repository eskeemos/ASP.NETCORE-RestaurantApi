using RestaurantApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApi
{
    public class RestaurantSeeder
    {
        private readonly DBContext context;
        public RestaurantSeeder(DBContext context)
        {
            this.context = context;
        }
        public void Seed()
        {
            if(context.Database.CanConnect())
            {
                if (!context.Roles.Any())
                {
                    var roles = GetRoles();
                    context.Roles.AddRange(roles);
                    context.SaveChanges();
                }

                if (!context.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    context.Restaurants.AddRange(restaurants);
                    context.SaveChanges();
                }
            }
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            { 
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Manager"
                },
                new Role()
                {
                    Name = "Admin"
                }
            };

            return roles;
        }

        public IEnumerable<Restaurant> GetRestaurants()
        {
            var restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name = "Restaurant1",
                    Description = "Description1",
                    Category = "Category1",
                    HasDelivery = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "DishName1",
                            Price = 10.50M
                        },
                        new Dish()
                        {
                            Name = "DishName2",
                            Price = 15.10M
                        }
                    },
                    Address = new Address()
                    {
                        City = "City1",
                        Street = "Street1",
                        PostalCode = "PostalCode1"
                    }
                },
                new Restaurant()
                {
                    Name = "Restaurant2",
                    Description = "Description2",
                    Category = "Category2",
                    HasDelivery = false,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "DishName1",
                            Price = 10.50M
                        },
                        new Dish()
                        {
                            Name = "DishName2",
                            Price = 15.10M
                        }
                    },
                    Address = new Address()
                    {
                        City = "City2",
                        Street = "Street2",
                        PostalCode = "PostalCode2"
                    }
                }
            };
            
            return restaurants;
        }
    }
}
