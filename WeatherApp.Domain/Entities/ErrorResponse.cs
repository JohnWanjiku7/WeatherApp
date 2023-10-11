using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Domain.Entities
{
    public class ErrorResponse
    {
        public bool Success { get; set; }
        public ErrorDetails Error { get; set; }
    }
    public class ErrorDetails
    {
        public int Code { get; set; }
        public string Type { get; set; }
        public string Info { get; set; }
    }
}
