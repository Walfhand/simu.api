namespace Simu.Api.Features.Simulations.Domain.Strategies;

public class CreditStrategyResolver
{
    public ICreditStrategy Resolve(CreditType type)
    {
        return type switch
        {
            CreditType.Fixed => new FixedCreditStrategy(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}