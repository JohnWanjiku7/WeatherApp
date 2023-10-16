using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using WeatherApp.Application.DTO;
using WeatherApp.Application.Features.Weather.Requests.Query;
using WeatherApp.Application.Interface.Infrastructure;

namespace WeatherAPP.ManagementAPI.Controllers
{
    [Route("api/weather")]
    [ApiController]
    public class WeatherController : Controller
    {

       
        private readonly IMediator _Mediator;
       public WeatherController( IMediator mediator)
        {
          
            _Mediator = mediator;
        }
        //[HttpGet]

        //public async Task<ActionResult<WeatherResponseDto>> GetWeather(string name)
        //{
        //    var weather = await _Mediator.Send(new GetWeatherDataRequest{ locationName = name });
        //    return weather;
        //}
        [Route("/PDF")]
        [HttpGet]
        public async Task<ActionResult<WeatherResponseDto>> GetPDF(string name)
        {
            WeatherResponseDto weatherDto = await _Mediator.Send(new GetWeatherDataRequest { locationName = name });
           
         

            byte[] pdfContent = await _Mediator.Send(new GeneratePDFRequest { weatherResponseDto = weatherDto });

            if (pdfContent != null)
            {
                // Return the generated PDF as a file response.
                return File(pdfContent, "application/pdf", "generated.pdf");
            }
            return BadRequest("PDF generation failed.");
        }
        

    }
}
