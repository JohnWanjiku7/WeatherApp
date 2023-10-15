//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using WeatherApp.Application.DTO;
//using WeatherApp.Domain.Entities;
//using WeatherApp.Domain.Exceptions;
//using WeatherApp.Domain.Interface;

//namespace WeatherApp.Application.UseCases
//{
//    public class FetchWeatherDataUseCase
//    {
//        private readonly IWeatherDataService _weatherDataService;

//        public FetchWeatherDataUseCase(IWeatherDataService weatherDataService)
//        {
//            _weatherDataService = weatherDataService ?? throw new ArgumentNullException(nameof(weatherDataService));
//        }

//        public async Task<WeatherResponseDto> ExecuteAsync(string locationName)
//        {
//            try
//            {
//                // Use the injected WeatherDataService to fetch weather data
//                WeatherResponseDto weatherData = await _weatherDataService.GetWeatherDataAsync(locationName);

//                if (weatherData == null)
//                {

//                    throw new WeatherDomainException("\nFetch Weather Data Use Case Error", "Weather data received is null");
//                }

//                return weatherData;
//            }
//            catch (Exception ex)
//            {
//                // Handle exceptions related to weather data fetching
//                throw new WeatherDomainException("\nFetch Weather Data Use Case Error", ex.Message);
//            }
//        }
//    }
//}
