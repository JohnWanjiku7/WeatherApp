using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text;
using WeatherApp.Application.DTO;
using WeatherApp.Persistence.Repositories;


namespace WeatherApp.Test.PDF
{
    public class Tests
    {
        
        [Test]
        public async Task GeneratePdf_GeneratesPdfFileAsync()
        {
            // Arrange
            var weatherResponse = new WeatherResponseDto
            {
                Location = new LocationDto
                {
                    name = "New York",
                    country = "United States of America",
                    region = "New York",
                    lat = "40.714",
                    lon = "-74.006",
                    timezone_id = "America/New_York",
                    localtime = "2019-09-07 08:14",
                    localtime_epoch = 1567844040,
                    utc_offset = "-4.0"
                },
                Current = new CurrentDto
                {
                    ObservationTime = "12:14 PM",
                    Temperature = 13,
                    WeatherCode = 113,
                    WeatherIcons = new List<string>
                    {
                        "https://cdn.worldweatheronline.com/images/wsymbols01_png_64/wsymbol_0002_sunny_intervals.png"
                    },
                    WeatherDescriptions = new List<string>
                    {
                        "Sunny"
                    },
                    WindSpeed = 0,
                    WindDegree = 349,
                    WindDir = "N",
                    Pressure = 1010,
                    Precip = 0,
                    Humidity = 90,
                    Cloudcover = 0,
                    Feelslike = 13,
                    UVIndex = 4,
                    Visibility = 16,
                    IsDay = "Yes"
                }
            };
            PDFGenerator pdfGenerator = new PDFGenerator();
            byte[] pdfContent = await pdfGenerator.GeneratePdfAsync(weatherResponse);




            // Assert
            Assert.NotNull(pdfContent);
            Assert.IsNotEmpty(pdfContent);
            // You can add more assertions here to validate the content of the generated PDF if needed
        }

        [Test]
        public void DownloadImage_DownloadsImage()
        {
            // Arrange
            var imageUrl = "https://cdn.worldweatheronline.com/images/wsymbols01_png_64/wsymbol_0002_sunny_intervals.png";
            PDFGenerator pdfGenerator = new PDFGenerator();

            // Act
            var imageData = pdfGenerator.DownloadImage(imageUrl);

            // Assert
            // Add assertions to validate the downloaded image data
            Assert.IsNotNull(imageData, "DownloadImage test passed");
        }
        [Test]
        public void AddWeatherData_GeneratesWeatherDataInTable()
        {
            // Arrange
            var document = new Document();
            var weatherResponse = new WeatherResponseDto
            {
                Current = new CurrentDto
                {
                    ObservationTime = "12:14 PM",
                    Temperature = 13,

                }
            };
            var pdfGenerator = new PDFGenerator();

            // Act
            PdfPTable table = pdfGenerator.AddWeatherData(weatherResponse);

            // Assert
            // Verify that the table is generated as expected
            Assert.IsNotNull(table);

            // You can add more specific assertions about the table content here
            Assert.IsTrue(table.Rows.Count > 0);
            Assert.AreEqual(table.NumberOfColumns, 2);

            // Check if specific content is present in the table
            // Assert on the specific content of individual cells
            Assert.AreEqual("12:14 PM", GetCellContent(table, 0, 1)); // The cell at row 0, column 1
            Assert.AreEqual("13", GetCellContent(table, 1, 1)); // The cell at row 1, column 1

        }
        private string GetCellContent(PdfPTable table, int row, int column)
        {
            PdfPCell cell = table.GetRow(row).GetCells()[column];

            return cell.Phrase[0].ToString();
        }

        [Test]
        public void AddTitle_AddsTitleToDocument()
        {
            // Arrange
            var weatherResponse = new WeatherResponseDto
            {
                Location = new LocationDto
                {
                    name = "New York",
                    region = "New York",
                    localtime = "2019-09-07 08:14"
                }
            };
            var pdfGenerator = new PDFGenerator();
            var titleParagraph = pdfGenerator.AddTitle(weatherResponse);

            // Act
            // Create a new Document and add the title paragraph
            var document = new Document();
            var documentStream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, documentStream);
            document.Open();
            document.Add(titleParagraph);
            document.Close();

            // Decode the binary content to a string
            var binaryContent = documentStream.ToArray();
            string decodedContent;
            using (var stream = new MemoryStream(binaryContent))
            {
                using (var reader = new PdfReader(stream))
                {
                    decodedContent = Encoding.UTF8.GetString(reader.GetPageContent(1));
                }
            }

            // Assert
            // Verify that the decoded document content contains expected information
            Assert.IsTrue(decodedContent.Contains("Weather Report for New York New York"));
            Assert.IsTrue(decodedContent.Contains("As At 2019-09-07 08:14"));
        }


    }
}