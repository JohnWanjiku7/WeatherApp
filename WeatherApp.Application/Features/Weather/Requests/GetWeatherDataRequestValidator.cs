using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Application.Features.Weather.Requests.Query;

namespace WeatherApp.Application.Features.Weather.Requests
{
    public  class GetWeatherDataRequestValidator : AbstractValidator <GetWeatherDataRequest>
    {
        public GetWeatherDataRequestValidator()
        {
            RuleFor(p => p.locationName)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull().WithMessage("{PropertyName} cannot be null")
                .MaximumLength(50).WithMessage("{PropertyName} cannot exceed 50 {ComparisonValue} characters ");
        }
    }
}
