using System.ComponentModel.DataAnnotations;

namespace RestaurantApi.Models
{
    public class CreateDishModel
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int RestaurantId { get; set; }
    }
}
