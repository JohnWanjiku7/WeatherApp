
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WeatherApp.Application.Services;
using WeatherApp.Application.UseCases;
using WeatherApp.Pdf;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;

namespace WeatherApp.ConsoleUI
{
    class Program
    {
     
        static async Task Main(string[] args)

        {
            
            Console.WriteLine("Welcome to the WeatherApp Console UI!");

            while (true)
            {
                Console.Write("Enter a location (e.g., 'Nairobi'): ");
                string locationName = Console.ReadLine();
                //locationName = "Nairobi";
              
                if (string.IsNullOrWhiteSpace(locationName))
                {
                    Console.WriteLine("Please enter a valid location name.");
                    continue;
                }
                string fileName = "WeatherReport.pdf";
                Console.Write("Enter the preffered name of the output document (e.g., 'WeatherReport'):");
                fileName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    Console.WriteLine("Please enter a valid file name.");
                    continue;
                }
                else
                {
                    if (!fileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                    {
                        fileName += ".pdf"; // Append ".pdf" if it's not already there
                    }

                   
                }
                try
                {
                    // Initialize the use case with the WeatherDataService dependency
                    var fetchWeatherDataUseCase = new FetchWeatherDataUseCase(new WeatherDataService());

                    // Fetch weather data for the specified location
                    var weatherData = await fetchWeatherDataUseCase.ExecuteAsync(locationName);

                    if (weatherData != null)
                    {
                        //// Display the weather data on the console
                        //Console.WriteLine("\nWeather Data:");
                        //Console.WriteLine($"Location: {weatherData.Location.name}");
                        //Console.WriteLine($"Observation Time: {weatherData.Current.ObservationTime}");
                        //Console.WriteLine($"Temperature: {weatherData.Current.Temperature}°C");
                        //Console.WriteLine($"Weather Description: {weatherData.Current.WeatherDescriptions[0]}");
                        //Console.WriteLine($"Wind Speed: {weatherData.Current.WindSpeed} km/h");
                        //Console.WriteLine($"Wind Direction: {weatherData.Current.WindDir}");
                        //Console.WriteLine($"Pressure: {weatherData.Current.Pressure} hPa");
                        //Console.WriteLine($"Humidity: {weatherData.Current.Humidity}%");
                        //Console.WriteLine($"Cloudcover: {weatherData.Current.Cloudcover}%");
                        //Console.WriteLine($"Feels Like: {weatherData.Current.Feelslike}°C");
                        //Console.WriteLine($"UV Index: {weatherData.Current.UVIndex}");
                        //Console.WriteLine($"Visibility: {weatherData.Current.Visibility} km");
                        //Console.WriteLine($"Is Day: {weatherData.Current.IsDay}");
                        IPdfGenerator pdfGenerator = new PdfGenerator();
                      
                        string result = pdfGenerator.GeneratePdf(weatherData, fileName);
                        Console.WriteLine(result);
                        Console.WriteLine("\nPress Enter to continue or 'Q' to quit.");
                        var key = Console.ReadKey();
                        
                        if (key.KeyChar == 'Q' || key.KeyChar == 'q')
                        {
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Weather data not found for the specified location.");
                    }
                }
                catch (Exception ex)
                {
                    
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }
    }
}
