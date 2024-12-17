using FluentValidation;
using MediatR;
using QuickApi.Engine.Web.Endpoints.Impl;
using Simu.Api.Features.Simulations.StartSimulation.Results;
using Simu.Api.Shared.Services;
using Newtonsoft.Json;
using Simu.Api.Features.Simulations.Domain.Strategies;

namespace Simu.Api.Features.Simulations.StartSimulation.Endpoints;

public record StartSimulationRequest : IRequest<SimulationResult>
{
    public decimal Capital { get; set; }
    public int Duration { get; set; }
    public decimal AnnualIncome { get; set; }
    public CreditType? CreditType { get; set; }
}

public class StartSimulationRequestValidator : AbstractValidator<StartSimulationRequest>
{
    public StartSimulationRequestValidator()
    {
        RuleFor(x => x.AnnualIncome).NotEmpty();
        RuleFor(x => x.Capital).NotEmpty();
        RuleFor(x => x.Duration).NotEmpty();
        RuleFor(x => x.AnnualIncome).NotEmpty();
    }
}
public class StartSimulationEndpoint()
    : GetMinimalEndpoint<StartSimulationRequest, SimulationResult>("simulations");

public class StartSimulationEndpointHandler : IRequestHandler<StartSimulationRequest, SimulationResult>
{
    private readonly ICacheService _cacheService;
    private readonly CreditStrategyResolver _strategyResolver;

    public StartSimulationEndpointHandler(ICacheService cacheService, CreditStrategyResolver strategyResolver)
    {
        _cacheService = cacheService;
        _strategyResolver = strategyResolver;
    }
    public async Task<SimulationResult> Handle(StartSimulationRequest request, CancellationToken cancellationToken)
    {
        var strategy = _strategyResolver.Resolve(request.CreditType ?? CreditType.Fixed);
        strategy.ValidateConstraints(request.Capital, request.Duration, request.AnnualIncome);
        var cacheKey = $"Simulation_{request.Capital}_{request.AnnualIncome}_{request.Duration}";
        var cachedResult = await _cacheService.GetAsync(cacheKey);
        if (!string.IsNullOrEmpty(cachedResult))
            return JsonConvert.DeserializeObject<SimulationResult>(cachedResult)!;
        
        var simulation = strategy.GenerateSimulation(request.Capital, request.Duration, request.AnnualIncome);
        await _cacheService.SetAsync(cacheKey, JsonConvert.SerializeObject(simulation), TimeSpan.FromDays(1));
        return simulation;
    }
    
}