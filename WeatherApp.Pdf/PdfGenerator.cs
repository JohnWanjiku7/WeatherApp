using iTextSharp.text;
using iTextSharp.text.pdf;
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
                title.SpacingAfter = 10f;
                document.Add(title);

                // Add content to the PDF
                PdfPTable table = new PdfPTable(2);
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.WidthPercentage = 80;
                title.SpacingAfter = 10f;
                document.Add(title);

                // Add content to the PDF
                PdfPCell nameCell = new PdfPCell(new Phrase(propertyName, new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD)));

                nameCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                nameCell.Padding = 8;


                PdfPCell valueCell = new PdfPCell(new Phrase(propertyValue.ToString(), new Font(Font.FontFamily.HELVETICA, 12)));


                valueCell.BackgroundColor = BaseColor.WHITE;
                valueCell.Padding = 8;

                    PdfPCell valueCell = new PdfPCell(new Phrase(propertyValue.ToString(), new Font(Font.FontFamily.HELVETICA, 12)));
                    // Add colors and padding to cells for a modern design
                    nameCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                    valueCell.BackgroundColor = BaseColor.WHITE;
                    nameCell.Padding = 8;
                    valueCell.Padding = 8;

                    PdfPCell valueCell = new PdfPCell(new Phrase(propertyValue.ToString(), new Font(Font.FontFamily.HELVETICA, 12)));
                    // Add colors and padding to cells for a modern design
                    nameCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                    valueCell.BackgroundColor = BaseColor.WHITE;
                    nameCell.Padding = 8;
                    valueCell.Padding = 8;

                    PdfPCell valueCell = new PdfPCell(new Phrase(propertyValue.ToString(), new Font(Font.FontFamily.HELVETICA, 12)));
                    // Add colors and padding to cells for a modern design
                table.AddCell(nameCell);
                table.AddCell(valueCell);
            }

            return table;
        }
                if ((propertyName == "WeatherIcons" || propertyName == "Weather icon") && propertyValue is System.Collections.IList iconList && iconList.Count > 0)
        public byte[] DownloadImage(string imageUrl)
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
                    }
        private static byte[] DownloadImage(string imageUrl)
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
                    throw new PDFGeneratorExceptions("Error downloading image:", ex.Message);
                }
            }
        }
    }
}