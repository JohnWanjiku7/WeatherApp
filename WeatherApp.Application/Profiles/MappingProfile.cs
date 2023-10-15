using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Application.DTO;
using WeatherApp.Domain.Entities;

namespace WeatherApp.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
                CreateMap<WeatherResponseDto, WeatherResponse>().ReverseMap();
                CreateMap<CurrentDto, Current>().ReverseMap();
                CreateMap<LocationDto, Location>().ReverseMap();
                CreateMap<LocationDto, Location>().ReverseMap();

                CreateMap<ErrorResponseDto, ErrorResponse>().ReverseMap();
                CreateMap<ErrorDetailsDto, ErrorDetails>().ReverseMap();
        }
    }
}
