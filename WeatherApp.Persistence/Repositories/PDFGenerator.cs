using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WeatherApp.Application.DTO;
using WeatherApp.Application.Interface.Persistence;
using WeatherApp.Domain.Entities;

namespace WeatherApp.Persistence.Repositories
{
    public class PDFGenerator : IGeneratePDF
    {
        public async Task<byte[]> GeneratePdfAsync(WeatherResponseDto weatherResponseDto)
        {
            try
            {


                using (var memoryStream = new MemoryStream())
                {
                    using (var document = new Document())
                    {
                        PdfWriter.GetInstance(document, memoryStream);
                        document.Open();
                        var title = AddTitle(weatherResponseDto);
                        document.Add(title);
                        PdfPTable table = AddWeatherData(weatherResponseDto);
                        document.Add(table);
                        document.Close();
                    }

                    // Convert the content of the MemoryStream to a byte array.
                    return memoryStream.ToArray();
                }
            }
            catch (Exception ex)
            {
                return Array.Empty<byte>();
                //throw new NotImplementedException();
                // throw new PDFGeneratorExceptions("Error creating the PDF:", ex.Message);
            }
        }
        public Paragraph AddTitle(WeatherResponseDto weatherResponseDto)
        {
            StringBuilder titleBuilder = new StringBuilder($"Weather Report for {weatherResponseDto.Location.name} {weatherResponseDto.Location.region}\nAs At {weatherResponseDto.Location.localtime}");
            var title = new Paragraph(titleBuilder.ToString(), new Font(Font.FontFamily.HELVETICA, 24, Font.BOLD, BaseColor.BLACK));
            title.Alignment = Element.ALIGN_CENTER;
            title.SpacingAfter = 10f;
            return title; ;
        }

        public PdfPTable AddWeatherData(WeatherResponseDto weatherResponseDto)
        {
            PdfPTable table = new PdfPTable(2)
            {
                DefaultCell = { Border = iTextSharp.text.Rectangle.NO_BORDER },
                WidthPercentage = 80
            };

            foreach (var prop in weatherResponseDto.Current.GetType().GetProperties())
            {
                string propertyName = prop.Name;
                var propertyValue = prop.GetValue(weatherResponseDto.Current);

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
                    //throw new PDFGeneratorExceptions("Error downloading image:", ex.Message);
                    throw new NotImplementedException();
                }
            }
        }
    }
}
