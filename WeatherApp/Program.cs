namespace WeatherApp.ConsoleUI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to the WeatherApp Console UI!");

            //var serviceProvider = ConfigureServices();
            //var weatherApp = serviceProvider.GetRequiredService<WeatherAppProgram>();

            //await weatherApp.RunAsync();
        }

        //static IServiceProvider ConfigureServices()
        //{
        //    var services = new ServiceCollection();
        //    // Register your services here
        //    services.AddSingleton<IWeatherDataService, WeatherDataService>();
        //    services.AddScoped<FetchWeatherDataUseCase>();
        //    services.AddSingleton<IPdfGenerator, PdfGenerator>();
        //    services.AddScoped<WeatherAppProgram>(); // Add WeatherApp to the services

        //    // Add additional service registrations

        //    return services.BuildServiceProvider();
        //}
    }
    //public class WeatherAppProgram
    //{
    //    private readonly FetchWeatherDataUseCase _fetchWeatherDataUseCase;

    //    private readonly IPdfGenerator _pdfGenerator;

    //    public WeatherAppProgram(FetchWeatherDataUseCase fetchWeatherDataUseCase, IPdfGenerator pdfGenerator)
    //    {
    //        _fetchWeatherDataUseCase = fetchWeatherDataUseCase;
    //        _pdfGenerator = pdfGenerator;
    //    }

    //    public async Task RunAsync()
    //    {
    //        while (true)
    //        {
    //            var locationName = ReadNonEmptyInput("Enter a location (e.g., 'Nairobi'): ");
    //            var fileName = ReadFileNameInput("Enter the preferred name of the output document (e.g., 'WeatherReport.pdf'): ");

    //            try
    //            {
    //                var weatherData = await _fetchWeatherDataUseCase.ExecuteAsync(locationName);

    //                if (weatherData != null)
    //                {
    //                    string result = _pdfGenerator.GeneratePdf(weatherData, fileName);
    //                    Console.WriteLine(result);

    //                    if (ShouldQuit())
    //                    {
    //                        break;
    //                    }
    //                }
    //                else
    //                {
    //                    Console.WriteLine("Weather data not found for the specified location.");
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                Console.WriteLine($"An error occurred: {ex.Message}");
    //            }
    //        }
    //    }

    //    static string ReadNonEmptyInput(string prompt)
    //    {
    //        string input;
    //        do
    //        {
    //            Console.Write(prompt);
    //            input = Console.ReadLine();
    //        } while (string.IsNullOrWhiteSpace(input));
    //        return input;
    //    }

    //    static string ReadFileNameInput(string prompt)
    //    {
    //        string fileName = ReadNonEmptyInput(prompt);

    //        if (!fileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
    //        {
    //            fileName += ".pdf"; // Append ".pdf" if it's not already there
    //        }

    //        return fileName;
    //    }

    //    static bool ShouldQuit()
    //    {
    //        Console.WriteLine("\nPress Enter to continue or 'Q' to quit.");
    //        var key = Console.ReadKey();
    //        return key.KeyChar == 'Q' || key.KeyChar == 'q';
    //    }
    //}

}