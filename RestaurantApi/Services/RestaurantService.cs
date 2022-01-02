using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Entities;
using RestaurantApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantApi.Services
{
    public interface IRestaurantService
    {
        RestaurantDto GetSingle(int id);
        IEnumerable<RestaurantDto> GetAll();
        int Create(CreateRestaurantDto dto);
        bool Delete(int id);
        bool Update(int id, UpdateRestaurantDto dto);
    }

    public class RestaurantService : IRestaurantService
    {
        private readonly DBContext context;
        private readonly IMapper mapper;

        public RestaurantService(DBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public RestaurantDto GetSingle(int id)
        {
            var restaurant = context.Restaurants
                .Include(x => x.Address)
                .Include(x => x.Dishes)
                .FirstOrDefault(x => x.Id == id);

            if (restaurant is null) return null;

            return mapper.Map<RestaurantDto>(restaurant);
        }

        public IEnumerable<RestaurantDto> GetAll()
        {
            var restaurants = context.Restaurants
                .Include(x => x.Address)
                .Include(x => x.Dishes)
                .ToList();

            return mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
        }

        public int Create(CreateRestaurantDto dto)
        {
            var restaurant = mapper.Map<Restaurant>(dto);
            context.Restaurants.Add(restaurant);
            context.SaveChanges();

            return restaurant.Id;
        }

        public bool Delete(int id)
        {
            var restaurant = context.Restaurants
                .FirstOrDefault(x => x.Id == id);

            if (restaurant is null) return false;

            context.Restaurants.Remove(restaurant);
            context.SaveChanges();

            return true;
        }

        public bool Update(int id, UpdateRestaurantDto dto)
        {
            var restaurant = context.Restaurants
                .FirstOrDefault(x => x.Id == id);

            if (restaurant is null) return false;

            restaurant.Name = dto.Name;
            restaurant.Description = dto.Description;
            restaurant.HasDelivery = dto.HasDelivery;

            context.SaveChanges();

            return true;
        }
    }
}
