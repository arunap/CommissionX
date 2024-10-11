using CommissionX.Application.Queries;
using CommissionX.Application.Strategies;
using CommissionX.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CommissionX.Application.Configurations
{
    public static class DependencyRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // register fluent validations for the models
            //  services.AddValidatorsFromAssemblyContaining<CreateEmployeeCommandValidator>();

            //  services.AddFluentValidationAutoValidation();

            // register mediator dependency
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetCommisionByInvoiceQuery).Assembly));

            services.AddScoped<ICommissionAggregator, CommissionAggregator>();

            return services;
        }
    }
}