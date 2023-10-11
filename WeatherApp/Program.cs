using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using WeatherApp.Application.UseCases;
using WeatherApp.Pdf;
using WeatherApp.Domain.Interface;
using WeatherApp.Application.Services;

namespace WeatherApp.ConsoleUI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to the WeatherApp Console UI!");

            var serviceProvider = ConfigureServices();
            var fetchWeatherDataUseCase = serviceProvider.GetRequiredService<FetchWeatherDataUseCase>();
            var pdfGenerator = serviceProvider.GetRequiredService<IPdfGenerator>();

            while (true)
            {
                var locationName = ReadNonEmptyInput("Enter a location (e.g., 'Nairobi'): ");
                var fileName = ReadFileNameInput("Enter the preferred name of the output document (e.g., 'WeatherReport.pdf'): ");

                try
                {
                    var weatherData = await fetchWeatherDataUseCase.ExecuteAsync(locationName);

                    if (weatherData != null)
                    {
                        string result = pdfGenerator.GeneratePdf(weatherData, fileName);
                        Console.WriteLine(result);

                        if (ShouldQuit())
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

        static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Register your services here
            services.AddSingleton<IWeatherDataService, WeatherDataService>();
            services.AddScoped<FetchWeatherDataUseCase>();
            services.AddSingleton<IPdfGenerator, PdfGenerator>();

            // Add additional service registrations

            return services.BuildServiceProvider();
        }

        static string ReadNonEmptyInput(string prompt)
        {
            string input;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(input));
            return input;
        }

        static string ReadFileNameInput(string prompt)
        {
            string fileName = ReadNonEmptyInput(prompt);

            if (!fileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                fileName += ".pdf"; // Append ".pdf" if it's not already there
            }

            return fileName;
        }

        static bool ShouldQuit()
        {
            Console.WriteLine("\nPress Enter to continue or 'Q' to quit.");
            var key = Console.ReadKey();
            return key.KeyChar == 'Q' || key.KeyChar == 'q';
        }
    }
}
