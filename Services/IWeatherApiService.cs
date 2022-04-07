using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherDemo.Models;

namespace WeatherDemo.Services
{
    public interface IWeatherApiService
    {
        Task<ServiceResponse> GetWeather(string country);
    }
}
