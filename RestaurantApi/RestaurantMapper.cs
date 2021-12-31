using AutoMapper;
using RestaurantApi.Entities;
using RestaurantApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApi
{
    public class RestaurantMapper : Profile
    {
        public RestaurantMapper()
        {
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(m => m.City, x => x.MapFrom(x => x.Address.City))
                .ForMember(m => m.Street, x => x.MapFrom(x => x.Address.Street))
                .ForMember(m => m.PostalCode, x => x.MapFrom(x => x.Address.PostalCode));

            CreateMap<Dish, DishDto>();
            CreateMap<CreateRestaurantDto, Restaurant>()
                .ForMember(m => m.Address, x => x.MapFrom(d => new Address()
                {
                    City = d.City,
                    Street = d.Street,
                    PostalCode = d.PostalCode
                }));
        }
    }
}
