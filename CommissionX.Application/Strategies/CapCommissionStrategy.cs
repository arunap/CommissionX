using CommissionX.Core.Entities;
using CommissionX.Core.Entities.Comissions;
using CommissionX.Core.Enums;
using CommissionX.Core.Interfaces;

namespace CommissionX.Application.Strategies
{
    public class CapCommissionStrategy : ICommissionRule
    {
        private readonly CapCommissionRule _capRule;
        private readonly ICommissionAggregator _aggregator;

        public CapCommissionStrategy(ICommissionAggregator aggregator, CapCommissionRule capRule)
        {
            _capRule = capRule;
            _aggregator = aggregator;
        }

        public decimal CalculateCommission(Invoice invoice, SalesPerson salesPerson)
        {
            decimal totalCommission = _aggregator.CalculateTotalCommission(invoice, salesPerson);

            var capAmt = _capRule.RateType == RateTypes.FLAT_RATE ? _capRule.Value : invoice.TotalAmount * (_capRule.Value / 100);

            return totalCommission <= capAmt ? totalCommission : capAmt;
        }
    }
}