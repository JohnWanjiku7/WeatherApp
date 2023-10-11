using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;
using WeatherApp.Domain;
using System.Net;
using System.Reflection;
using System.Net.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WeatherApp.Domain.Entities;
using System.Text;
using WeatherApp.Domain.Exceptions;
using WeatherApp.Pdf.PDFExceptions;

namespace WeatherApp.Pdf
{
    public class PdfGenerator : IPdfGenerator
    {
        public string GeneratePdf(WeatherResponse weatherResponse, string fileName)
        {
           
           
           if( (fileName.Equals(string.Empty)))
            {
                 fileName = "WeatherReport2.pdf";
            }
            
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
               string outputPath = Path.Combine(desktopPath, fileName);
            
          
            Current weatherData = weatherResponse.Current;

            Document document = new Document();

            // Get the type of the object
            Type type = typeof(Current);

            try
            {
                PdfWriter.GetInstance(document, new FileStream(outputPath, FileMode.Create));
                document.Open();

                // Set a custom title for the document
                // Create a StringBuilder to build the title string
                StringBuilder titleBuilder = new StringBuilder("Weather Report for ");
                titleBuilder.Append(weatherResponse.Location.name);
                titleBuilder.Append(" ");
                titleBuilder.Append(weatherResponse.Location.region);
                titleBuilder.Append("\nAs At ");
                titleBuilder.Append(weatherResponse.Location.localtime);

                // Create the title paragraph using the StringBuilder
                Paragraph title = new Paragraph(titleBuilder.ToString(), new Font(Font.FontFamily.HELVETICA, 24, Font.BOLD, BaseColor.BLACK));

                title.Alignment = Element.ALIGN_CENTER;
                title.SpacingAfter = 10f;
                document.Add(title);

                // Add content to the PDF
                PdfPTable table = new PdfPTable(2);
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.WidthPercentage = 80;

                foreach (PropertyInfo prop in type.GetProperties())
                {
                    string propertyName = prop.Name;
                    object propertyValue = prop.GetValue(weatherData);

                    if (propertyValue == null)
                        continue;
                    var attribute = prop.GetCustomAttribute<DisplayAttribute>();
                    if (attribute != null)
                    {
                        // Use the custom display name if available, otherwise use the property name
                        propertyName = attribute.Description;
                    }


                    PdfPCell nameCell = new PdfPCell(new Phrase(propertyName, new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD)));
                    PdfPCell valueCell = new PdfPCell(new Phrase(propertyValue.ToString(), new Font(Font.FontFamily.HELVETICA, 12)));
                    // Add colors and padding to cells for a modern design
                    nameCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                    valueCell.BackgroundColor = BaseColor.WHITE;
                    nameCell.Padding = 8;
                    valueCell.Padding = 8;


                    if ((propertyName == "WeatherIcons" || propertyName == "Weather icon") && propertyValue is System.Collections.IList iconList && iconList.Count > 0)
                    {
                        string iconUrl = iconList[0].ToString();
                        byte[] imageData = DownloadImage(iconUrl);

                        if (imageData != null)
                        {
                            // Create a MemoryStream from the image data
                            using (MemoryStream ms = new MemoryStream(imageData))
                            {
                                // Load the image from the MemoryStream
                                System.Drawing.Image originalImage = System.Drawing.Image.FromStream(ms);

                                // Define the new width and height for the resized image
                                int newWidth = 400;
                                int newHeight = 400;

                                // Create a new Bitmap with the specified dimensions
                                using (System.Drawing.Image resizedImage = new System.Drawing.Bitmap(originalImage, newWidth, newHeight))
                                {
                                    // Convert the resized image to a byte array
                                    using (MemoryStream resizedMs = new MemoryStream())
                                    {
                                        resizedImage.Save(resizedMs, System.Drawing.Imaging.ImageFormat.Png);
                                        byte[] resizedImageData = resizedMs.ToArray();

                                        // Create an iTextSharp Image from the resized image data
                                        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(resizedImageData);
                                        valueCell = new PdfPCell(image, true);
                                    }
                                }
                            }
                        }
                    }

                    if ((propertyName == "WeatherDescriptions" || propertyName == "Weather Description") && propertyValue is System.Collections.IList descriptionList && descriptionList.Count > 0)
                    {
                        string descriptions = descriptionList[0].ToString();

                        valueCell = new PdfPCell(new Phrase(descriptions));
                    }

                    table.AddCell(nameCell);
                    table.AddCell(valueCell);
                }

                document.Add(table);
            }
            catch (Exception ex)
            {
                throw new PDFGeneratorExceptions("Error creating the PDF:", ex.Message);
            }
            finally
            {
                document.Close();
            }

            return $"PDF created at: {Path.GetFullPath(outputPath)}";
            
        }

        private static byte[] DownloadImage(string imageUrl)
        {
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    return webClient.DownloadData(imageUrl);
                }
                catch (Exception ex)
                {
                    
                    throw new PDFGeneratorExceptions("\nError downloading image:", ex.Message);
                    
                }
            }
        }


    }
}
