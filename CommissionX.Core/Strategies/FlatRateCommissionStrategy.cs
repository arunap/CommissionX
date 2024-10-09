using CommissionX.Core.Constants;
using CommissionX.Core.Entities;
using CommissionX.Core.Entities.Comissions;
using CommissionX.Core.Interfaces;

namespace CommissionX.Core.Strategies
{
    public class FlatRateCommissionStrategy : ICommissionRuleStrategy
    {

        private static readonly List<Product> products = new List<Product>()
        {
            new() { Id =  Guid.Parse("9EC033D3-59CB-4407-9901-53A678835293"), Name = "Product-1", Price = 5m },
            new() { Id =  Guid.Parse("A09F8F83-CF8F-4D16-B564-CC758E821AEB"), Name = "Product-2", Price = 4m },
            new() { Id =  Guid.Parse("1C826F0D-AAA0-4DD9-9322-4791945E1BF7"), Name = "Product-3", Price = 7m },
            new() { Id =  Guid.Parse("7D66A139-F565-41F2-9EA9-6CB248FFE923"), Name = "Product-4", Price = 10m },
        };

        private static readonly List<CommisionBase> temp = new()
        {
            new FlatCommisionRule{  RuleScope = CommisionRuleScopeTypes.INVOICE, Value = 5m },
            new FlatCommisionRule{  RuleScope = CommisionRuleScopeTypes.PRODUCT, Value = 2m },
            new FlatCommisionRule{  RuleScope = CommisionRuleScopeTypes.MULTIPLES_OF_A_PRODUCT, Value = 2m },
            new FlatCommisionRule{  RuleScope = CommisionRuleScopeTypes.MULTIPLES_OF_A_PRODUCT, Value = 3m },
        };

        private static readonly Invoice invoice = new()
        {
            Id = Guid.NewGuid(),
            SalesPerson = new() { Id = Guid.NewGuid(), Name = "Aruna", CommissionRules = temp },
            InvoiceProducts = new List<InvoiceProduct>
            {
                new() { InvoiceId = Guid.NewGuid(), ProductId = Guid.Parse("9EC033D3-59CB-4407-9901-53A678835293"), Quantity = 3 },
                new() { InvoiceId = Guid.NewGuid(), ProductId = Guid.Parse("7D66A139-F565-41F2-9EA9-6CB248FFE923"), Quantity = 5 }
            },
        };

        private readonly List<FlatCommisionRule> _commissionRules;

        public FlatRateCommissionStrategy(List<FlatCommisionRule> commissionRules) => _commissionRules = commissionRules;

        public decimal CalculateCommission(SalesPerson salesPerson, Invoice invoice)
        {
            decimal totalCommission = 0;

            foreach (var rule in _commissionRules)
            {
                if (rule.RuleScope == CommisionRuleScopeTypes.INVOICE)
                {
                    totalCommission += rule.Value;
                }
                else
                {
                    var product = invoice.InvoiceProducts.FirstOrDefault(p => p.ProductId == rule.ProductId);
                    if (product == null)
                        continue;

                    if (rule.RuleScope == CommisionRuleScopeTypes.PRODUCT)
                    {
                        // Calculate commission per Product
                        totalCommission += rule.Value;
                    }
                    else if (rule.RuleScope == CommisionRuleScopeTypes.MULTIPLES_OF_A_PRODUCT)
                    {

                        totalCommission += rule.Value * product.Quantity;
                    }
                }
            }

            return totalCommission;
        }
    }
}