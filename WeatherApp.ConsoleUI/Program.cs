using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;

class Program
{
    static async Task Main(string[] args)
    {
        while (true)
        {
            var locationName = ReadNonEmptyInput("Enter a location (e.g., 'Nairobi'): ");
            var customFileName = ReadFileNameInput("Enter a custom file name for the PDF (or press Enter to use default): ");

            string apiUrl = "http://localhost:5035/PDF";
            string queryString = $"?name={Uri.EscapeDataString(locationName)}";
            apiUrl += queryString;

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        byte[] pdfBytes = await response.Content.ReadAsByteArrayAsync();

                        // Determine the PDF file name
                        string pdfFilePath;
                        if (!string.IsNullOrWhiteSpace(customFileName))
                        {
                            pdfFilePath = customFileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase)
                                ? customFileName
                                : customFileName + ".pdf";
                        }
                        else
                        {
                            pdfFilePath = "WeatherReport.pdf";
                        }

                        File.WriteAllBytes(pdfFilePath, pdfBytes);

                        Console.WriteLine($"PDF downloaded successfully as {pdfFilePath}");
                    }
                    else
                    {
                        Console.WriteLine("Weather data not found for the specified location.");
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"HTTP request error: {ex.Message}");
                }
            }

            if (ShouldQuit())
            {
                break;
            }
        }
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
        Console.Write(prompt);
        return Console.ReadLine().Trim();
    }

    static bool ShouldQuit()
    {
        Console.WriteLine("\nPress Enter to continue or 'Q' to quit.");
        var key = Console.ReadKey();
        return key.KeyChar == 'Q' || key.KeyChar == 'q';
    }
}
