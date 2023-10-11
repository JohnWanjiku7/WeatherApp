using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Domain.Exceptions
{
    public class WeatherDomainException : Exception
    {
        public WeatherDomainException()
        {
        }
        public WeatherDomainException(string message) 
        {
        }

     


      

        public WeatherDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
        public WeatherDomainException(string message, string errorMessage) : base($"{message} Error: {errorMessage}")
        {
        }
    }
}

