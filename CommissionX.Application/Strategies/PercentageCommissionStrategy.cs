using CommissionX.Core.Entities;
using CommissionX.Core.Entities.Comissions;
using CommissionX.Core.Enums;
using CommissionX.Core.Interfaces;

namespace CommissionX.Application.Strategies
{
    public class PercentageCommissionStrategy : ICommissionRule
    {
        private readonly List<PercentageCommisionRule> _commissionRules;

        public PercentageCommissionStrategy(List<PercentageCommisionRule> commissionRules) => _commissionRules = commissionRules;

        public decimal CalculateCommission(Invoice invoice, SalesPerson salesPerson)
        {
            decimal totalCommission = 0;

            foreach (var rule in _commissionRules)
            {
                if (rule.RuleScope == RuleScopeTypes.INVOICE)
                {
                    totalCommission += invoice.TotalAmount * rule.Value / 100;
                }
                // Calculate commission for individual Products
                else if (rule.RuleScope == RuleScopeTypes.PRODUCT)
                {
                    var product = invoice.InvoiceProducts.FirstOrDefault(p => p.ProductId == rule.ProductId);
                    if (product == null)
                        continue;

                    // Calculate commission based on price and quantity of the product
                    totalCommission += product.Product.Price * (rule.Value / 100);
                }
            }

            return totalCommission;
        }
    }
}