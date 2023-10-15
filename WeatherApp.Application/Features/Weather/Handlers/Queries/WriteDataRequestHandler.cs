using System.Threading;
using System.Threading.Tasks;
using AutoMapper; // Ensure you have the appropriate using statement for AutoMapper.
using MediatR;
using WeatherApp.Application.DTO;
using WeatherApp.Application.Features.Weather.Requests;
using WeatherApp.Application.Features.Weather.Requests.Query;
using WeatherApp.Application.Interface.Infrastructure;
using WeatherApp.Application.Interface.Persistence;

namespace WeatherApp.Application.Features.Weather.Handlers.Queries
{
    public class WriteDataRequestHandler : IRequestHandler<WriteDataRequest>
    {
        private readonly IWriteData _writeData;
        private readonly IMapper _mapper;

        public WriteDataRequestHandler(IWriteData writeData, IMapper mapper)
        {
            _writeData = writeData;
            _mapper = mapper;
        }

        public async Task Handle(WriteDataRequest request, CancellationToken cancellationToken)
        {
            // You may want to map request to WeatherResponseDto if needed.
            // var weatherDataDto = _mapper.Map<WeatherResponseDto>(request);

            // Call the WriteDataAsync method and handle the result if necessary.
            var weatherData = await _writeData.WriteDataAsync(request.WeatherResponseDto);

            // Handle weatherData as needed.
        }
    }
}
