using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Pdf.PDFExceptions
{
    public class PDFGeneratorExceptions: Exception
    {
        public PDFGeneratorExceptions()
        {
        }
        public PDFGeneratorExceptions(string message)
        {
        }


        public PDFGeneratorExceptions(string message, Exception innerException) : base(message, innerException)
        {
        }
        public PDFGeneratorExceptions(string message, string errorMessage) : base($"{message} Error: {errorMessage}")
        {
        }
    }
}
