using CommissionX.Core.Entities;
using CommissionX.Core.Entities.Rules;
using CommissionX.Core.Enums;
using CommissionX.Core.Interfaces;

namespace CommissionX.Application.Strategies
{
    public class CapCommissionStrategy : ICommissionRule
    {
        private readonly List<CommissionRule> _capRules;
        private readonly ICommissionAggregator _aggregator;

        public CapCommissionStrategy(ICommissionAggregator aggregator, List<CommissionRule> capRules)
        {
            _capRules = capRules;
            _aggregator = aggregator;
        }

        public decimal CalculateCommission(Invoice invoice)
        {
            decimal totalCommission = _aggregator.CalculateTotalCommission(invoice);

            return ApplyCap(_capRules, totalCommission, invoice.TotalAmount);
        }

        private static decimal ApplyCap(List<CommissionRule> commissionRules, decimal totalCommission, decimal invoiceAmt)
        {
            if (commissionRules.Count == 0) return totalCommission;

            // give priority to individual rules
            var capRules = commissionRules.Any(r => r.SalesPersonCommissionRules.Any()) ?
                            commissionRules.Where(r => r.SalesPersonCommissionRules.Any()) :
                            commissionRules.Where(r => !r.SalesPersonCommissionRules.Any());

            // calculate min cap rule
            var minCap = commissionRules.GroupBy(r => r.RateCalculationType)
            .Select(group => group.Min(cr => group.Key == RateCalculationType.Fixed ? cr.Value : invoiceAmt * (cr.Value / 100))).Min();

            return totalCommission <= minCap ? totalCommission : minCap;
        }
    }
}