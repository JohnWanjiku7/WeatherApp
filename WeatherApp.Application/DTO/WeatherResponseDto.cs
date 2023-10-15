using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Domain.Entities;

namespace WeatherApp.Application.DTO
{
    public class WeatherResponseDto
    {

        public LocationDto Location { get; set; }
        public CurrentDto Current { get; set; }
        public RequestDto Request { get; set; }
    }
}
