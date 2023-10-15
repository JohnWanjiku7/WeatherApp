using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Application.DTO;

namespace WeatherApp.Application.Interface.Persistence
{
    public interface IWriteData
    {
        Task<bool> WriteDataAsync(WeatherResponseDto weatherResponseDto);
    }
}
