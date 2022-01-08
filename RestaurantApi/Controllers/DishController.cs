using Microsoft.AspNetCore.Mvc;
using RestaurantApi.Models;
using RestaurantApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApi.Controllers
{
    [Route("api/restaurant/{restaurantId}/dish")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService dishService;

        public DishController(IDishService dishService)
        {
            this.dishService = dishService;
        }

        [HttpPost]
        public ActionResult Post([FromRoute] int restaurantId, [FromBody] CreateDishModel model)
        {
            var dishId = dishService.Create(restaurantId, model);

            return Created($"api/{restaurantId}/dish/{dishId}", null);
        }

        [HttpGet]
        public ActionResult<List<DishModel>> GetAll([FromRoute] int restaurantId)
        {
            return Ok(dishService.GetAll(restaurantId));
        }

        [HttpGet("{id}")]
        public ActionResult<DishModel> GetSingle([FromRoute] int restaurantId, [FromRoute] int id)
        {
            return Ok(dishService.GetSingle(restaurantId, id));
        }

        [HttpDelete]
        public ActionResult Delete([FromRoute] int restaurantId, int id)
        {
            dishService.Remove(restaurantId, id);
            return NoContent();
        }
    }
}
