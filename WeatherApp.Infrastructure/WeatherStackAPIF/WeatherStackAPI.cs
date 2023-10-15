using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Application.DTO;
using WeatherApp.Application.Interface.Infrastructure;
using WeatherApp.Application.Models;

namespace WeatherApp.Infrastructure.WeatherStackAPIF
{
    public class WeatherStackAPI : IWeatherStackAPI
    {
        private WeatherStackAPISettings _WeatherStackAPiSettings { get; }
        public WeatherStackAPI(IOptions<WeatherStackAPISettings> _WeatherStackAPiSettings)
        {
                
        }

        public Task<WeatherResponseDto> GetWeatherDataAsync(string locationName)
        {
            throw new NotImplementedException();
        }
    }
}
