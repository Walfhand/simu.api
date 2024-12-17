namespace Simu.Api.Features.Simulations.StartSimulation.Results;

public record SimulationResult
{
    public decimal FixedAnnualRate { get; set; }
    public decimal MonthlyAmount { get; set; }
    public List<DepreciationTableLine> DepreciationTableLines { get; set; } = [];
}

public record DepreciationTableLine(string Month, decimal MonthlyAmount, decimal InterestShare, decimal CapitalShare, decimal RemainingBalance);