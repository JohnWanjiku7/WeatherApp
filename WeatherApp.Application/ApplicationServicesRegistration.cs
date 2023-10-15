using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.DependencyInjection;


using Microsoft.Extensions.DependencyInjection;
using WeatherApp.Application.Profiles;

namespace WeatherApp.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection  ConfigureApplicationServices (this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            return services;
        }
    }
}

