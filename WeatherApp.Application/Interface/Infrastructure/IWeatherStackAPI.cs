using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Application.DTO;

namespace WeatherApp.Application.Interface.Infrastructure
{
    public interface IWeatherStackAPI
    {
        Task<WeatherResponseDto> GetWeatherDataAsync(string locationName);
    }
}
