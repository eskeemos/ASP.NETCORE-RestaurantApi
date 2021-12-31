using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Entities;
using RestaurantApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantApi.Controllers
{
    [Route("api/restaurant")]
    public class RestaurantController : ControllerBase
    {
        private readonly DBContext context;
        private readonly IMapper mapper;

        public RestaurantController(DBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            var restaurants = context.Restaurants
                .Include(x => x.Address)
                .Include(x => x.Dishes)
                .ToList();
            var restaurantsDto = mapper.Map<List<RestaurantDto>>(restaurants);

            return restaurantsDto;
        }

        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> GetSingle([FromRoute] int id)
        {
            var restaurant = context.Restaurants
                .Include(x => x.Address)
                .Include(x => x.Dishes)
                .FirstOrDefault(x => x.Id == id);
            if (restaurant is null) return NotFound();

            var restaurantDto = mapper.Map<RestaurantDto>(restaurant);

            return Ok(restaurantDto);
        }

        [HttpPost]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
            var restaurant = mapper.Map<Restaurant>(dto);
            context.Restaurants.Add(restaurant);
            context.SaveChanges();
            return Created($"/api/restaurant/{restaurant.Id}", null);
        }
    }
}
