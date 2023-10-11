using System.Text.Json;
using Newtonsoft.Json;
using WeatherApp.Domain.Entities;
using WeatherApp.Domain.Interface;
using WeatherApp.Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WeatherApp.Application.Services
{
    public class WeatherDataService : IWeatherDataService
    {
        //private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _apiUrl;

        public WeatherDataService()
        {

            var serviceCollection = new ServiceCollection();

            IConfiguration configuration;
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json")
                .Build();



            serviceCollection.AddSingleton<IConfiguration>(configuration);

            string apiKey = (configuration["WeatherApi:ApiKey"]);
            string apiUrl = (configuration["WeatherApi:EndpointUrl"]);

            //_httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
            _apiUrl = apiUrl ?? throw new ArgumentNullException(nameof(apiUrl));
        }

        public async Task<WeatherResponse> GetWeatherDataAsync(string locationName)
        {
            HttpClient _httpClient = new HttpClient();
            try
            {
                // Construct the API URL with the access key and location query
                string apiUrl = $"{_apiUrl}/current?access_key={_apiKey}&query={locationName}";

                // Send an HTTP GET request to the API
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
              
                
                    

                    // Read the response content as a JSON string
                    string responseBody = await response.Content.ReadAsStringAsync();

                ErrorResponse errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseBody);
                if (errorResponse.Error ==null)
                { 
                    // Deserialize the JSON response into a WeatherResponse object
                    WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(responseBody);

                    // Extract and return the relevant WeatherData
                    return weatherResponse;
                }
                else
                {
                    // Request failed with an error status code
                    string errorMessage = $"Status code: {errorResponse.Error.Code}\nStatus info:{errorResponse.Error.Info}\nStatus info:{errorResponse.Error.Type}";

                   

                    // Throw an exception or handle the error as needed
                    throw new WeatherDomainException($"\nWeather Data Service Error: Error Response Code", errorMessage);
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle HTTP request errors
                throw new WeatherDomainException($"\nWeather Data Service Error: HTTP Request" ,ex.Message);
            }
            catch (Newtonsoft.Json.JsonException ex)
            {
                // Handle JSON deserialization errors
                throw new WeatherDomainException("\nWeather Data Service Error: Converting JSON", ex.Message);
            }
        }


    }
}
