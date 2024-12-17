using FluentValidation;
using FluentValidation.AspNetCore;
using QuickApi.Engine.Web;
using QuickApi.Engine.Web.Cqrs;
using Simu.Api.Cqrs;
using Simu.Api.Features.Simulations.StartSimulation.Endpoints;
using Simu.Api.Configs.ProblemDetails;
using Simu.Api.Configs.Redis;
using Simu.Api.Features.Simulations.Domain.Strategies;
using Simu.Api.Shared.Services;

namespace Simu.Api.Configs;

public static class ApplicationConfig
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCustomProblemDetails();
        services.AddMinimalEndpoints();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
        services.AddScoped<IMessage, MessageService>();
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<StartSimulationRequestValidator>();
        services.AddRedis(configuration);
        services.AddScoped<ICacheService, RedisCacheService>();
        services.AddSingleton<CreditStrategyResolver>();
        return services;
    }
}