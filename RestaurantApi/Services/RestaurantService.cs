﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantApi.Entities;
using RestaurantApi.Exceptions;
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
        void Delete(int id);
        void Update(int id, UpdateRestaurantDto dto);
    }

    public class RestaurantService : Controller, IRestaurantService
    {
        private readonly DBContext context;
        private readonly IMapper mapper;
        private readonly ILogger<RestaurantService> logger;

        public RestaurantService(DBContext context, IMapper mapper, ILogger<RestaurantService> logger)
        {
            this.context = context;
            this.mapper = mapper;
            this.logger = logger;
        }

        public RestaurantDto GetSingle(int id)
        {
            var restaurant = context.Restaurants
                .Include(x => x.Address)
                .Include(x => x.Dishes)
                .FirstOrDefault(x => x.Id == id);

            if (restaurant is null) throw new NotFoundException("Restaurant not found");

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

        public void Delete(int id)
        {
            logger.LogWarning($"Restaurant with id : {id} DELETE action invoked");

            var restaurant = context.Restaurants
                .FirstOrDefault(x => x.Id == id);

            if (restaurant is null) throw new NotFoundException("Restaurant not found");

            context.Restaurants.Remove(restaurant);
            context.SaveChanges();
        }

        public void Update(int id, UpdateRestaurantDto dto)
        {
            var restaurant = context.Restaurants
                .FirstOrDefault(x => x.Id == id);

            if (restaurant is null) throw new NotFoundException("Restaurant not found");

            restaurant.Name = dto.Name;
            restaurant.Description = dto.Description;
            restaurant.HasDelivery = dto.HasDelivery;

            context.SaveChanges();
        }
    }
}
