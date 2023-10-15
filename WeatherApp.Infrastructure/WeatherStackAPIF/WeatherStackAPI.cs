using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Application.DTO;
using WeatherApp.Application.Interface.Infrastructure;
using WeatherApp.Application.Models;
using WeatherApp.Domain.Entities;
using WeatherApp.Domain.Exceptions;

namespace WeatherApp.Infrastructure.WeatherStackAPIF
{
    public class WeatherStackAPI : IWeatherStackAPI
    {
        private WeatherStackAPISettings _WeatherStackAPiSettings { get; }
        public WeatherStackAPI(IOptions<WeatherStackAPISettings> WeatherStackAPiSettings)
        {
            _WeatherStackAPiSettings = WeatherStackAPiSettings.Value;
        }



        public async Task<WeatherResponseDto> GetWeatherDataAsync(string locationName)
        {
            string apiUrl = _WeatherStackAPiSettings.EndpointUrl;
            string apiKey = _WeatherStackAPiSettings.ApiKey;
            HttpClient _httpClient = new HttpClient();
            try
            {
                //Construct the API URL with the access key and location query
                string requestURl = $"{apiUrl}/current?access_key={apiKey}&query={locationName}";

                // Send an HTTP GET request to the API
                HttpResponseMessage response = await _httpClient.GetAsync(requestURl);
                response.EnsureSuccessStatusCode();


                //Read the response content as a JSON string
                string responseBody = await response.Content.ReadAsStringAsync();

                ErrorResponse errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseBody);
                if (errorResponse.Error == null)
                {
                    // Deserialize the JSON response into a WeatherResponse object
                    WeatherResponseDto weatherResponseDto = JsonConvert.DeserializeObject<WeatherResponseDto>(responseBody);

                    //Return the relevant WeatherData
                    return weatherResponseDto;
                }
                else
                {
                    // Request failed with an error status code
                    string errorMessage = $"Status code: {errorResponse.Error.Code}\nStatus info:{errorResponse.Error.Info}\nStatus info:{errorResponse.Error.Type}";

                    //Throw an exception or handle the error as needed
                    throw new WeatherDomainException($"\nWeather Data Service Error: Error Response Code", errorMessage);
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle HTTP request errors
                throw new WeatherDomainException($"\nWeather Data Service Error: HTTP Request", ex.Message);
            }
            catch (JsonException ex)
            {
                //Handle JSON deserialization errors
                throw new WeatherDomainException("\nWeather Data Service Error: Converting JSON", ex.Message);
            }
        }


        
    }
}
