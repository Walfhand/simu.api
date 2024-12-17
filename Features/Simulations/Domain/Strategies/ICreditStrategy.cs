using Simu.Api.Features.Simulations.StartSimulation.Results;

namespace Simu.Api.Features.Simulations.Domain.Strategies;

public interface ICreditStrategy
{
    SimulationResult GenerateSimulation(decimal capital, int duration, decimal annualIncome);
    void ValidateConstraints(decimal capital, int duration, decimal annualIncome);
}