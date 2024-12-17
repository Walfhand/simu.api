using Simu.Api.Shared;

namespace Simu.Api.Features.Simulations.StartSimulation.Exceptions;

public class SimulationException(string message) : BusinessException(message)
{
    
}