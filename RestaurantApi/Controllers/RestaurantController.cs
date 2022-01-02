using Microsoft.AspNetCore.Mvc;
using RestaurantApi.Models;
using RestaurantApi.Services;
using System.Collections.Generic;

namespace RestaurantApi.Controllers
{
    [Route("api/restaurant")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            this.restaurantService = restaurantService;
        }
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            return Ok(restaurantService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> GetSingle([FromRoute] int id)
        {
            var result = restaurantService.GetSingle(id);

            if (result is null) return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var id = restaurantService.Create(dto);

            return Created($"/api/restaurant/{id}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            bool isDeleted = restaurantService.Delete(id);
            if (isDeleted) return NoContent();
            return NotFound();
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] UpdateRestaurantDto dto)
        {
            if (ModelState.IsValid) return BadRequest(ModelState);

            bool isUpdated = restaurantService.Update(id, dto);
            if (isUpdated) return Ok();
            return NotFound();
        }
    }
}
