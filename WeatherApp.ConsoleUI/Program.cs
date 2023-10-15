using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http.Headers;
using System.Xml.Linq;

class Program
{
    static async Task Main(string[] args)
    {

        while (true)
        {
            var locationName = ReadNonEmptyInput("Enter a location (e.g., 'Nairobi'): ");

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

                        // Save the downloaded PDF to a file or process it as needed.
                        string pdfFilePath = "downloaded.pdf";
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
