using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Application.Features.Weather.Requests.Query;
using WeatherApp.Application.Interface.Persistence;

namespace WeatherApp.Application.Features.Weather.Handlers.Queries
{
 
    public class GeneratePDFRequesthandler : IRequestHandler<GeneratePDFRequest>
    {
        private readonly IGeneratePDF _generatePDF;
        private readonly IMapper _mapper;

        public GeneratePDFRequesthandler(IGeneratePDF generatePDF, IMapper mapper)
        {
            _generatePDF = generatePDF;
            _mapper = mapper;
        }

        public async Task Handle(GeneratePDFRequest request, CancellationToken cancellationToken)
        {
            // You may want to map request to WeatherResponseDto if needed.
            // var weatherDataDto = _mapper.Map<WeatherResponseDto>(request);

            // Call the WriteDataAsync method and handle the result if necessary.
            var weatherData = await _generatePDF.GeneratePdfAsync(request.WeatherResponseDto);

            // Handle weatherData as needed.
        }

       
    }
}
