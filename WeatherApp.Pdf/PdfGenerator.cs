using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using iTextSharp.text;
using iTextSharp.text.pdf;
using WeatherApp.Domain.Entities;
using WeatherApp.Domain.Exceptions;
using WeatherApp.Pdf.PDFExceptions;
using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Pdf
{
    public class PdfGenerator : IPdfGenerator
    {
        public string GeneratePdf(WeatherResponse weatherResponse, string fileName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    fileName = "WeatherReport2.pdf";
                }

                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string outputPath = Path.Combine(desktopPath, fileName);

                Document document = new Document();
                PdfWriter.GetInstance(document, new FileStream(outputPath, FileMode.Create));

                document.Open();
                var title = AddTitle(weatherResponse);
                document.Add(title);
                PdfPTable table = AddWeatherData( weatherResponse);
                document.Add(table);
                document.Close();

                return $"PDF created at: {Path.GetFullPath(outputPath)}";
            }
            catch (Exception ex)
            {
                throw new PDFGeneratorExceptions("Error creating the PDF:", ex.Message);
            }
        }

        public Paragraph AddTitle( WeatherResponse weatherResponse)
        {
            StringBuilder titleBuilder = new StringBuilder($"Weather Report for {weatherResponse.Location.name} {weatherResponse.Location.region}\nAs At {weatherResponse.Location.localtime}");
            var title = new Paragraph(titleBuilder.ToString(), new Font(Font.FontFamily.HELVETICA, 24, Font.BOLD, BaseColor.BLACK));
            title.Alignment = Element.ALIGN_CENTER;
            title.SpacingAfter = 10f;
            return title; ;
        }

        public PdfPTable AddWeatherData( WeatherResponse weatherResponse)
        {
            PdfPTable table = new PdfPTable(2)
            {
                DefaultCell = { Border = Rectangle.NO_BORDER },
                WidthPercentage = 80
            };

            foreach (var prop in weatherResponse.Current.GetType().GetProperties())
            {
                string propertyName = prop.Name;
                var propertyValue = prop.GetValue(weatherResponse.Current);

                if (propertyValue == null) continue;

                var attribute = (DisplayAttribute)Attribute.GetCustomAttribute(prop, typeof(DisplayAttribute));
                propertyName = attribute?.Name ?? propertyName;

                PdfPCell nameCell = new PdfPCell(new Phrase(propertyName, new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD)));

                nameCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                nameCell.Padding = 8;


                PdfPCell valueCell = new PdfPCell(new Phrase(propertyValue.ToString(), new Font(Font.FontFamily.HELVETICA, 12)));


                valueCell.BackgroundColor = BaseColor.WHITE;
                valueCell.Padding = 8;


                if ((propertyName == "WeatherIcons" || propertyName == "Weather icon") && propertyValue is System.Collections.IList iconList && iconList.Count > 0)
                {
                    byte[] imageData = DownloadImage(iconList[0].ToString());
                    if (imageData != null)
                    {
                        var image = iTextSharp.text.Image.GetInstance(imageData);
                        valueCell = new PdfPCell(image, true);
                    }
                }

                if ((propertyName == "WeatherDescriptions" || propertyName == "Weather Description") && propertyValue is System.Collections.IList descriptionList && descriptionList.Count > 0)
                {
                    valueCell = new PdfPCell(new Phrase(descriptionList[0].ToString()));
                }

                table.AddCell(nameCell);
                table.AddCell(valueCell);
            }

            return table;
        }

        public byte[] DownloadImage(string imageUrl)
        {
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    return webClient.DownloadData(imageUrl);
                }
                catch (Exception ex)
                {
                    throw new PDFGeneratorExceptions("Error downloading image:", ex.Message);
                }
            }
        }
    }
}