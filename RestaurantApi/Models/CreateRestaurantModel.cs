using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApi.Models
{
    public class CreateRestaurantModel
    {
        [Required][MaxLength(25)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool HasDelivery { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }
        [Required][MaxLength(50)]
        public string City { get; set; }
        [Required][MaxLength(50)]
        public string Street { get; set; }
        public string PostalCode { get; set; }
    }
}
