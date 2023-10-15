using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Domain.Entities
{
    public class ErrorDetails
    {
        public int Code { get; set; }
        public string Type { get; set; }
        public string Info { get; set; }
    }
}
