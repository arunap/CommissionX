using CommissionX.Core.Entities;
using CommissionX.Core.Interfaces;

namespace CommissionX.Application.Strategies
{
    public class CommissionAggregator : ICommissionAggregator
    {
        private readonly List<ICommissionRule> _commissionRules;

        public CommissionAggregator() => _commissionRules = new List<ICommissionRule>();

        public void AddStrategry(ICommissionRule commissionRule) => _commissionRules.Add(commissionRule);

        public decimal CalculateTotalCommission(Invoice invoice, SalesPerson salesPerson)
        {
            decimal totalCommission = 0;

            foreach (var rule in _commissionRules)
            {
                totalCommission += rule.CalculateCommission(invoice, salesPerson);
            }

            return totalCommission;
        }
    }
}