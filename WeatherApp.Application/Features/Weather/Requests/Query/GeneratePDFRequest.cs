﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Application.DTO;

namespace WeatherApp.Application.Features.Weather.Requests.Query
{
    public class GeneratePDFRequest: IRequest<byte[]>
    {
        public WeatherResponseDto weatherResponseDto { get; set; }
    }
}
