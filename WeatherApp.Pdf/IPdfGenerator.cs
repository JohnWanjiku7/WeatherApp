using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Domain;
using WeatherApp.Domain.Entities;

namespace WeatherApp.Pdf
{
    public interface IPdfGenerator
    {
        string GeneratePdf(WeatherResponse weatherResponse, string outputPath);
    }
}
