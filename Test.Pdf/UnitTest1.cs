using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

using WeatherApp.Domain.Entities;
using WeatherApp.Pdf;
using System.IO;
using iTextSharp.text;
using Org.BouncyCastle.Asn1.Ocsp;

namespace WeatherApp.Tests
{
    [TestFixture]
    public class PdfGeneratorTests
    {
        [Test]
        public void GeneratePdf_GeneratesPdfFile()
        {
            // Arrange
            var weatherResponse = new WeatherResponse
            {
                var weatherData = new WeatherResponse
{
    Request = new Request
    {
        Type = "City",
        Query = "New York, United States of America",
        Language = "en",
        Unit = "m"
    },
    Location = new Location
    {
        Name = "New York",
        Country = "United States of America",
        Region = "New York",
        Lat = "40.714",
        Lon = "-74.006",
        TimezoneId = "America/New_York",
        Localtime = "2019-09-07 08:14",
        LocaltimeEpoch = 1567844040,
        UtcOffset = "-4.0"
    },
    Current = new Current
    {
        ObservationTime = "12:14 PM",
        Temperature = 13,
        WeatherCode = 113,
        WeatherIcons = new List<string>
        {
            "https://assets.weatherstack.com/images/wsymbols01_png_64/wsymbol_0001_sunny.png"
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
        UvIndex = 4,
        Visibility = 16
    }
};

        };

            var fileName = "TestReport.pdf";
            var outputPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);
            var pdfGenerator = new PdfGenerator();

            // Act
            string result = pdfGenerator.GeneratePdf(weatherResponse, fileName);

            // Assert
            Assert.IsTrue(File.Exists(outputPath));
            // You can add more assertions here to validate the content of the generated PDF if needed
        }

        // Add more test cases for the helper methods (AddTitle, AddWeatherData, DownloadImage) as needed
    }
}
