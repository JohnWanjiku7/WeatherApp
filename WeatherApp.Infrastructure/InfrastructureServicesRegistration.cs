using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Application.Interface.Infrastructure;
using WeatherApp.Application.Models;
using WeatherApp.Infrastructure.WeatherStackAPIF;

namespace WeatherApp.Infrastructure
{
    public static  class InfrastructureServicesRegistration
    {
       
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {




            services.AddSingleton(configuration);
            services.Configure<WeatherStackAPISettings>(configuration.GetSection("WeatherApi"));
            services.AddTransient<IWeatherStackAPI, WeatherStackAPI>();


           

            return services;
        }
    }

}
