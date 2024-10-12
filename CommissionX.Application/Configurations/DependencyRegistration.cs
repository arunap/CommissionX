using CommissionX.Application.Behaviors;
using CommissionX.Application.Commands;
using CommissionX.Application.Queries;
using CommissionX.Application.Strategies;
using CommissionX.Application.Validators;
using CommissionX.Core.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
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
            services.AddValidatorsFromAssemblyContaining<CreateCommissionRuleCommandValidator>();

            services.AddFluentValidationAutoValidation();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            // register mediator dependency
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetCommisionByInvoiceQuery).Assembly));

            services.AddScoped<ICommissionAggregator, CommissionAggregator>();

            return services;
        }
    }
}