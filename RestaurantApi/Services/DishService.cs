using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Entities;
using RestaurantApi.Exceptions;
using RestaurantApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantApi.Services
{
    public interface IDishService
    {
        int Create(int restaurantId, CreateDishModel createDish);
        DishModel GetSingle(int restaurantId, int id);
        IEnumerable<DishModel> GetAll(int restaurantId);
        void Remove(int restaurantId, int id);
    }
    public class DishService : IDishService
    {
        private readonly IMapper mapper;
        private readonly DBContext context;

        public DishService(IMapper mapper, DBContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }
        public int Create(int restaurantId, CreateDishModel dto)
        {
            var restaurant = GetRestaurantById(restaurantId);

            var dish = mapper.Map<Dish>(dto);

            dish.RestaurantId = restaurantId;
            context.Dishes.Add(dish);
            context.SaveChanges();

            return dish.Id;
        }

        public IEnumerable<DishModel> GetAll(int restaurantId)
        {
            var restaurant = GetRestaurantById(restaurantId);

            return mapper.Map<List<DishModel>>(restaurant.Dishes);
        }

        public DishModel GetSingle(int restaurantId, int id)
        {
            var restaurant = GetRestaurantById(restaurantId);

            var dish = restaurant.Dishes.FirstOrDefault(x => x.Id == id);

            if(dish is null || dish.RestaurantId != restaurantId)
            {
                throw new NotFoundException("Dish not found");
            }

            return mapper.Map<DishModel>(dish);
        }

        public void Remove(int restaurantId, int id)
        {
            var restaurant = GetRestaurantById(restaurantId);

            var dish = restaurant.Dishes.FirstOrDefault(x => x.Id == id);

            if (dish is null || dish.RestaurantId != restaurantId)
            {
                throw new NotFoundException("Dish not found");
            }

            restaurant.Dishes.Remove(dish);

            context.SaveChanges();
        }

        private Restaurant GetRestaurantById(int restaurantId)
        {
            var restaurant = context.Restaurants
                .Include(x => x.Dishes)
                .FirstOrDefault(x => x.Id == restaurantId);

            if (restaurant is null) throw new NotFoundException("Restaurant not found");

            return restaurant;
        }
    }
}
