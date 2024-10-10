using CommissionX.Core.Entities;
using CommissionX.Core.Entities.Comissions;
using CommissionX.Core.Enums;
using CommissionX.Core.Interfaces;

namespace CommissionX.Application.Strategies
{
    public class FlatCommissionStrategy : ICommissionRule
    {

        private readonly List<FlatCommissionRule> _commissionRules;

        public FlatCommissionStrategy(List<FlatCommissionRule> commissionRules) => _commissionRules = commissionRules;

        public decimal CalculateCommission(Invoice invoice, SalesPerson salesPerson)
        {
            decimal totalCommission = 0;

            foreach (var rule in _commissionRules)
            {
                if (rule.RuleScope == RuleScopeTypes.INVOICE)
                {
                    totalCommission += rule.Value;
                }
                else
                {
                    var product = invoice.InvoiceProducts.FirstOrDefault(p => p.ProductId == rule.ProductId);
                    if (product == null)
                        continue;

                    if (rule.RuleScope == RuleScopeTypes.PRODUCT)
                    {
                        // Calculate commission per Product
                        totalCommission += rule.Value;
                    }
                    else if (rule.RuleScope == RuleScopeTypes.MULTIPLES_OF_A_PRODUCT)
                    {

                        totalCommission += rule.Value * product.Quantity;
                    }
                }
            }

            return totalCommission;
        }
    }
}