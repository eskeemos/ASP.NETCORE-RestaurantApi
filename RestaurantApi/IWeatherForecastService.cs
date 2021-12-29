using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApi
{
    public interface IWeatherForecastService
    {
        public IEnumerable<WeatherForecast> Get(int amount, int min, int max);
    }
}
