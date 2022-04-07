using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WeatherDemo.Models;
using WeatherDemo.Services;


namespace WeatherDemo.Controllers
{
    public class HomeController : Controller
    {

        private readonly IWeatherApiService _weatherApiService;

        public HomeController(IWeatherApiService weatherApiService)
        {
            _weatherApiService = weatherApiService;
        }

        public async Task<IActionResult> Index(string LocationName)
        {
            if (!String.IsNullOrEmpty(LocationName))
            {
                var serviceResponse = await _weatherApiService.GetWeather(LocationName);
             
                
                    return View(serviceResponse);
                
            }
            return View();
        }


    }
}
