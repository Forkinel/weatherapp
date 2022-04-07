using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherDemo.Models;
using Newtonsoft.Json;

namespace WeatherDemo.Services
{
    public class WeatherApiService : IWeatherApiService
    {
        private static readonly HttpClient client;

        static WeatherApiService()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri("http://api.openweathermap.org")
            };
        }

        public async Task<ServiceResponse> GetWeather(string location)
        {
            ServiceResponse serviceResponse = new ServiceResponse();
            var geoCodeURL = string.Format("/geo/1.0/direct?q={0}&limit=1&appid={1}", location, "373b1bb9cf546715025691d21cd4a362");
        
            var geoCodeResponse = await client.GetAsync(geoCodeURL);
            if (geoCodeResponse.IsSuccessStatusCode)
            {
                var stringResponse = await geoCodeResponse.Content.ReadAsStringAsync();
                if (stringResponse != "[]")
                {

                    List<GeoData> geoResult = JsonConvert.DeserializeObject<List<GeoData>>(stringResponse);

                    var weatherURL = string.Format("/data/2.5/weather?lat={0}&lon={1}&units=metric&appid={2}", geoResult[0].lat, geoResult[0].lon, "373b1bb9cf546715025691d21cd4a362");

                    var weatherResponse = await client.GetAsync(weatherURL);
                    if (weatherResponse.IsSuccessStatusCode)
                    {

                        var stringWeatherResponse = await weatherResponse.Content.ReadAsStringAsync();
                        WeatherData weatherResult = JsonConvert.DeserializeObject<WeatherData>(stringWeatherResponse);
                        weatherResult.location = location;

                        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                        DateTime dateTimeRise = dateTime.AddSeconds(weatherResult.sys.sunrise).ToLocalTime();
                        DateTime dateTimeSet = dateTime.AddSeconds(weatherResult.sys.sunset).ToLocalTime();

                        weatherResult.convertedSunriseTime = dateTimeRise.ToString("H:mm");
                        weatherResult.convertedSunsetTime = dateTimeSet.ToString("H:mm");


                        serviceResponse.WeatherData = weatherResult;
                        serviceResponse.IsSuccess = true;
                    }
                }
                else
                {
                    serviceResponse.ErrorMessage = "Location not found";
                    serviceResponse.IsSuccess = false;
                }
            }
            else
            {
                serviceResponse.ErrorMessage = $"There has been an unknown error - {geoCodeResponse.ReasonPhrase}";
                serviceResponse.IsSuccess = false;

            }
            return serviceResponse;
        }
    }
}
