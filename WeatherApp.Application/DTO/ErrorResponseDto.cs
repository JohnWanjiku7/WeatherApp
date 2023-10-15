using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Domain.Entities;

namespace WeatherApp.Application.DTO
{
    public class ErrorResponseDto
    {
        public bool Success { get; set; }
        public ErrorDetailsDto Error { get; set; }
    }
}
