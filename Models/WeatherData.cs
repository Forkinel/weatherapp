using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherDemo.Models
{
    public class WeatherData
    {
        public string location { get; set; }
        public Main main{ get; set; }
        public Sys sys { get; set; }

        public string convertedSunriseTime { get; set;}
        public string convertedSunsetTime { get; set; }



    }
}
