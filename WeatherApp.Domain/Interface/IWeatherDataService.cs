
using WeatherApp.Domain.Entities;

namespace WeatherApp.Domain.Interface
{
    public interface IWeatherDataService
    {
        Task<WeatherResponse> GetWeatherDataAsync(string query);
    }
}
