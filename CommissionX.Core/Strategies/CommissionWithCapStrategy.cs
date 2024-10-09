using CommissionX.Core.Constants;
using CommissionX.Core.Entities;
using CommissionX.Core.Entities.Comissions;
using CommissionX.Core.Interfaces;

namespace CommissionX.Core.Strategies
{
    public class CommissionWithCapStrategy
    {
        private readonly CapCommisionRule _capRule;

        public CommissionWithCapStrategy(CapCommisionRule capRule)
        {
            _capRule = capRule;
        }

        public decimal CalculateCommission(decimal totalCommission, Invoice invoice)
        {
            var capValue = _capRule.Value;
            var cap = _capRule.RateType == CommisionRateTypes.FLAT_RATE ? capValue : invoice.TotalAmount * (capValue / 100);
            
            return totalCommission <= cap ? totalCommission : cap;
        }
    }
}