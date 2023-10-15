using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Application.DTO;
using WeatherApp.Application.Interface.Persistence;

namespace WeatherApp.Persistence.Repositories
{
    public class WriteData : IWriteData
    {
        public Task<bool> WriteDataAsync(WeatherResponseDto weatherResponseDto)
        {
            throw new NotImplementedException();
        }
    }
}
