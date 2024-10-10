using CommissionX.Application.Queries;
using CommissionX.Application.Strategies;
using CommissionX.Core.Entities.Comissions;
using CommissionX.Core.Interfaces;
using MediatR;
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

            // Register your specific QueryHandler if not using automatic scanning
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetCommissionRulesByTypeQuery<>).Assembly));

            services
                .AddTransient<IRequestHandler<GetCommissionRulesByTypeQuery<FlatCommissionRule>, List<FlatCommissionRule>>, GetCommissionRulesByTypeQueryHandler<FlatCommissionRule>>()
                .AddTransient<IRequestHandler<GetCommissionRulesByTypeQuery<PercentageCommisionRule>, List<PercentageCommisionRule>>, GetCommissionRulesByTypeQueryHandler<PercentageCommisionRule>>()
                .AddTransient<IRequestHandler<GetCommissionRulesByTypeQuery<CapCommissionRule>, List<CapCommissionRule>>, GetCommissionRulesByTypeQueryHandler<CapCommissionRule>>();

            services.AddScoped<ICommissionAggregator, CommissionAggregator>();

            return services;
        }
    }
}