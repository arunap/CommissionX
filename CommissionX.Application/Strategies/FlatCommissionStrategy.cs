using CommissionX.Core.Entities;
using CommissionX.Core.Entities.Rules;
using CommissionX.Core.Enums;
using CommissionX.Core.Interfaces;

namespace CommissionX.Application.Strategies
{
    public class FlatCommissionStrategy : ICommissionRule
    {

        private readonly List<CommissionRule> _commissionRules;

        public FlatCommissionStrategy(List<CommissionRule> commissionRules) => _commissionRules = commissionRules;

        public decimal CalculateCommission(Invoice invoice)
        {
            decimal totalCommission = 0;

            foreach (var rule in _commissionRules)
            {
                if (rule.RuleContextType == RuleContextType.Invoice)
                {
                    totalCommission += rule.Value;
                }
                else
                {
                    var product = invoice.InvoiceProducts.FirstOrDefault(p => p.ProductId == rule.ProductId);
                    if (product == null)
                        continue;

                    if (rule.RuleContextType == RuleContextType.Product)
                    {
                        // Calculate commission per Product
                        totalCommission += rule.Value;
                    }
                    else if (rule.RuleContextType == RuleContextType.ProductMultiples)
                    {
                        totalCommission += rule.Value * product.Quantity;
                    }
                }
            }

            return totalCommission;
        }
    }
}