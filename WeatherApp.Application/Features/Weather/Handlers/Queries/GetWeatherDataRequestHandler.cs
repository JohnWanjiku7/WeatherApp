using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Application.DTO;
using WeatherApp.Application.Exceptions;
using WeatherApp.Application.Features.Weather.Requests;
using WeatherApp.Application.Features.Weather.Requests.Query;
using WeatherApp.Application.Interface;
using WeatherApp.Application.Interface.Infrastructure;
using WeatherApp.Application.Interface.Persistence;

namespace WeatherApp.Application.Features.Weather.Handlers.Queries
{
    public class GetWeatherDataRequestHandler : IRequestHandler<GetWeatherDataRequest, WeatherResponseDto>
    {
        

       
        private readonly IMapper _mapper;
        private readonly IWeatherStackAPI _weatherStackAPI;

        public GetWeatherDataRequestHandler(IMapper mapper, IWeatherStackAPI weatherStackAPI)
        {
            _mapper = mapper;
            _weatherStackAPI = weatherStackAPI;
        }

        public async Task<WeatherResponseDto> Handle(GetWeatherDataRequest request, CancellationToken cancellationToken)
        {
            var validator = new GetWeatherDataRequestValidator();
            var validationResult = await validator.ValidateAsync(request);   
            if (validationResult.IsValid == false)
            {
                throw new ValidationException(validationResult);
            }
            var weatherData = await _weatherStackAPI.GetWeatherDataAsync(request.locationName);
            return _mapper.Map <WeatherResponseDto>(weatherData);

        }
    }
}
