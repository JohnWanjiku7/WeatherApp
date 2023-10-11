using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Domain.Entities
{
    public class WeatherResponse
    {
        public Location Location { get; set; }
        public Current Current { get; set; }
    }
}
