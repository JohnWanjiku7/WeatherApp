using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Application.DTO;
using WeatherApp.Application.Features.Weather.Requests.Query;
using WeatherApp.Application.Interface.Persistence;

namespace WeatherApp.Application.Features.Weather.Handlers.Queries
{
 
    public class GeneratePDFRequesthandler : IRequestHandler<GeneratePDFRequest, byte[]>
    {
        private readonly IGeneratePDF _generatePDF;
      

        public GeneratePDFRequesthandler(IGeneratePDF generatePDF)
        {
            _generatePDF = generatePDF;
          
        }



        public async Task<byte[]> Handle(GeneratePDFRequest request, CancellationToken cancellationToken)
        {
           

            // Call the GeneratePdfAsync method and return the result.
            var PDFstream = await _generatePDF.GeneratePdfAsync(request.weatherResponseDto);

            return PDFstream;
        }

    }
}
