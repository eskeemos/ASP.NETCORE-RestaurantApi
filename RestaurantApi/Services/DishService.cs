using AutoMapper;
using RestaurantApi.Entities;
using RestaurantApi.Exceptions;
using RestaurantApi.Models;
using System.Linq;

namespace RestaurantApi.Services
{
    public interface IDishService
    {
        int Create(int restaurantId, CreateDishModel createDish);
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
            var restaurant = context.Restaurants
                .FirstOrDefault(x => x.Id == restaurantId);

            if (restaurant is null) throw new NotFoundException("Restaurant not found");

            var dish = mapper.Map<Dish>(dto);

            dish.RestaurantId = restaurantId;
            context.Dishes.Add(dish);
            context.SaveChanges();

            return dish.Id;
        }
    }
}
