using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherApp.Application.Interface;
using WeatherApp.Application.Interface.Persistence;
using WeatherApp.Persistence.Repositories;

namespace WeatherApp.Persistence
{
    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection ConfigurePesistenceServices (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IWriteData, WriteData>();
            return services;
        }
    }
}
