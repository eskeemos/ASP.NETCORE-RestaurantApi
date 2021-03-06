using Microsoft.AspNetCore.Mvc;
using RestaurantApi.Models;
using RestaurantApi.Services;
using System.Collections.Generic;

namespace RestaurantApi.Controllers
{
    [Route("api/restaurant")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            this.restaurantService = restaurantService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<RestaurantModel>> GetAll()
        {
            return Ok(restaurantService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<RestaurantModel> GetSingle([FromRoute] int id)
        {
            return Ok(restaurantService.GetSingle(id));
        }

        [HttpPost]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantModel dto)
        {
            var id = restaurantService.Create(dto);

            return Created($"/api/restaurant/{id}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            restaurantService.Delete(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] UpdateRestaurantModel dto)
        {
            restaurantService.Update(id, dto);

            return Ok();
        }
    }
}
