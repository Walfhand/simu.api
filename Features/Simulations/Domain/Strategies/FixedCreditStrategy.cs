using System.Globalization;
using Simu.Api.Features.Simulations.StartSimulation.Exceptions;
using Simu.Api.Features.Simulations.StartSimulation.Results;

namespace Simu.Api.Features.Simulations.Domain.Strategies;

public class FixedCreditStrategy : ICreditStrategy
{
    private static decimal DetermineMonthlyRate(decimal income)
    {
        return income switch
        {
            <= 16400 => 0.00141656M, 
            <= 19700 => 0.00158320M, 
            <= 23000 => 0.00174983M, 
            <= 27800 => 0.00191646M,
            <= 32700 => 0.00208309M,
            <= 43200 => 0.00224972M,
            _ => 0.00241635M
        };
    }
    
    public void ValidateConstraints(decimal capital, int duration, decimal income)
    {
        var errors = new List<string>();

        if (capital is < 20000 or > 310000)
            errors.Add("The capital must be between 20,000€ and 310,000€.");

        if (duration is < 180 or > 360)
            errors.Add("The duration must be between 180 and 360 months.");

        if (income is < 0 or > 53900)
            errors.Add("The income must be between 0€ and 53,900€ per year.");

        if (errors.Count != 0)
            throw new SimulationException(string.Join(" ", errors));
    }
    
    public SimulationResult GenerateSimulation(decimal capital, int duration, decimal income)
    {
        var monthlyRate = DetermineMonthlyRate(income);
        var fixedAnnualRate = Math.Round(monthlyRate * 12 * 100, 2);
        var monthlyAmount = capital * monthlyRate / (1 - (decimal)Math.Pow(1 + (double)monthlyRate, -duration));

        var depreciationTable = new List<DepreciationTableLine>();
        decimal remainingBalance = capital;

        var startDate = DateTime.Now.AddMonths(1);

        for (int monthIndex = 0; monthIndex < duration; monthIndex++)
        {
            var currentMonth = startDate.AddMonths(monthIndex).ToString("MMMM yyyy", CultureInfo.CreateSpecificCulture("en-US"));
            var interestShare = Math.Round(remainingBalance * monthlyRate, 2);
            var capitalShare = Math.Round(monthlyAmount - interestShare, 2);
            remainingBalance = Math.Max(Math.Round(remainingBalance - capitalShare, 2), 0);

            depreciationTable.Add(new DepreciationTableLine(
                Month: currentMonth,
                MonthlyAmount: Math.Round(monthlyAmount, 2),
                InterestShare: interestShare,
                CapitalShare: capitalShare,
                RemainingBalance: remainingBalance
            ));
        }

        return new SimulationResult
        {
            FixedAnnualRate = fixedAnnualRate,
            MonthlyAmount = Math.Round(monthlyAmount, 2),
            DepreciationTableLines = depreciationTable
        };
    }
}