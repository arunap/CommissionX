using System.Text.Json.Serialization;
using CommissionX.Api.Middlewares;
using CommissionX.Application.Configurations;
using CommissionX.Infrastructure.Configurations;
using CommissionX.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers()
            .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var errors = context.ModelState.Where(m => m.Value.Errors.Count > 0).SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage);

                        var response = new { Success = false, Message = "Validation errors occurred", Errors = errors };

                        return new BadRequestObjectResult(response);
                    };
                })
            .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo
{
    Version = "v1",
    Title = "Commission API",
    Description = "API for managing Commission Rules and Calculations",
    TermsOfService = new Uri("https://example.com/terms"),
    Contact = new OpenApiContact
    {
        Name = "API Support",
        Email = "support@example.com",
        Url = new Uri("https://example.com/support"),
    },
    License = new OpenApiLicense
    {
        Name = "Use under LICX",
        Url = new Uri("https://example.com/license"),
    }
}));

var app = builder.Build();

app.UseExceptionMiddleware();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Commission API v1");
        c.RoutePrefix = "swagger";
    });
}

using var sp = app.Services.CreateScope();
if (!app.Configuration.GetValue<bool>("UseInMemoryDatabase"))
{
    var databaseService = sp.ServiceProvider.GetRequiredService<CommissionDataContext>();
    databaseService.Database.Migrate();
}

var ruledata = sp.ServiceProvider.GetRequiredService<SeedDataInitializer>();
await ruledata.InitializeAsync();

app.UseAuthorization();

app.MapControllers();

app.Run();
