//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Newtonsoft.Json;
//using WeatherApp.Application.DTO;
//using WeatherApp.Application.Interface;
//using WeatherApp.Domain.Entities;
//using WeatherApp.Domain.Exceptions;

//namespace WeatherApp.Infrastructure.Services
//{
//    public class GetWeatherData : IGetWeatherData
//    {
//        private readonly string _apiKey;
//        private readonly string _apiUrl;

//        public GetWeatherData()
//        {

//            var serviceCollection = new ServiceCollection();

//            IConfiguration configuration;
//            configuration = new ConfigurationBuilder()
//                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
//                .AddJsonFile("appsettings.json")
//                .Build();

//            serviceCollection.AddSingleton(configuration);



//            _apiKey = configuration["WeatherApi:ApiKey"] ?? throw new ArgumentNullException("ApiKey is missing in appsettings.json");
//            _apiUrl = configuration["WeatherApi:EndpointUrl"] ?? throw new ArgumentNullException("EndpointUrl is missing in appsettings.json");
//        }

//        public async Task<WeatherResponseDto> GetWeatherDataAsync(string locationName)
//        {
//            HttpClient _httpClient = new HttpClient();
//            try
//            {
//                //Construct the API URL with the access key and location query
//                string apiUrl = $"{_apiUrl}/current?access_key={_apiKey}&query={locationName}";

//               // Send an HTTP GET request to the API
//                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
//                response.EnsureSuccessStatusCode();


//                //Read the response content as a JSON string
//                string responseBody = await response.Content.ReadAsStringAsync();

//                ErrorResponse errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseBody);
//                if (errorResponse.Error == null)
//                {
//                   // Deserialize the JSON response into a WeatherResponse object
//                   WeatherResponseDto weatherResponse = JsonConvert.DeserializeObject<WeatherResponseDto>(responseBody);

//                    //Return the relevant WeatherData
//                    return weatherResponse;
//                }
//                else
//                {
//                   // Request failed with an error status code
//                    string errorMessage = $"Status code: {errorResponse.Error.Code}\nStatus info:{errorResponse.Error.Info}\nStatus info:{errorResponse.Error.Type}";

//                    //Throw an exception or handle the error as needed
//                    throw new WeatherDomainException($"\nWeather Data Service Error: Error Response Code", errorMessage);
//                }
//            }
//            catch (HttpRequestException ex)
//            {
//               // Handle HTTP request errors
//                throw new WeatherDomainException($"\nWeather Data Service Error: HTTP Request", ex.Message);
//            }
//            catch (JsonException ex)
//            {
//                //Handle JSON deserialization errors
//                throw new WeatherDomainException("\nWeather Data Service Error: Converting JSON", ex.Message);
//            }


//        }
//    }
//}