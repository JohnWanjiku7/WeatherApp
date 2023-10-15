using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Application.DTO;
using WeatherApp.Domain.Entities;

namespace WeatherApp.Application.Interface.Persistence
{
    public interface IGeneratePDF
    {
        
        Task<bool> GeneratePdfAsync(WeatherResponseDto weatherResponseDto);
    }
}

