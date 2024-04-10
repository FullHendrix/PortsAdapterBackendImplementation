using CompanyName.DDD.Domain.Common.Log;
using CompanyName.DDD.Domain.Common.Log.LogImplementations;
using CompanyName.DDD.Domain.TaxTypeAggregate.Application;
using CompanyName.DDD.Domain.TaxTypeAggregate.Application.Interface;
using CompanyName.DDD.Domain.TaxTypeAggregate.Infrastructure.SQLServer;
namespace CompanyName.DDD.API.Extensions
{
    public static class DIExtension
    {
        public static IServiceCollection AddConfigurations(this IServiceCollection services)
        {
            services.AddTransient<ITaxTypeAggregate, TaxTypeAggregate>();
            services.AddTransient<ITaxTypeRepository, TaxTypeRepository>();
            services.AddTransient<ILog, StandartOuputLog>();
            return services;
        }
    }   
}