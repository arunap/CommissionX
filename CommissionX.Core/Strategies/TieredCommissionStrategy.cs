using CommissionX.Core.Constants;
using CommissionX.Core.Entities;
using CommissionX.Core.Entities.Comissions;
using CommissionX.Core.Interfaces;

namespace CommissionX.Core.Strategies
{
    public class TieredCommissionStrategy : ICommissionRuleStrategy
    {
        private readonly List<TireCommisionRule> _rules;
        public TieredCommissionStrategy(List<TireCommisionRule> tiers)
        {
            _rules = tiers;
        }

        public decimal CalculateCommission(SalesPerson salesPerson, Invoice invoice)
        {
            decimal commission = 0;
            foreach (var rule in _rules)
            {
                var commisionProduct = invoice.InvoiceProducts.FirstOrDefault(x => x.ProductId == rule.ProductId);
                if (commisionProduct == null) return 0;

                foreach (var tier in rule.Tires)
                {

                    if (tier.IsInRange(commisionProduct.Quantity))
                    {
                        // apply commision based on the total value of the invoice.
                        if (rule.RuleScope == CommisionRuleScopeTypes.INVOICE)
                        {
                            if (tier.RateType == CommisionRateTypes.FLAT_RATE)
                            {
                                commission += tier.Value;
                            }
                            else if (tier.RateType == CommisionRateTypes.PERCENTAGE)
                            {
                                commission += invoice.TotalAmount * tier.Value / 100;
                            }
                        }
                        // check whether the sold quantity is in the range
                        else if (rule.RuleScope == CommisionRuleScopeTypes.PRODUCT)
                        {
                            foreach (var specificProduct in invoice.InvoiceProducts.Where(x => x.Product.Id == commisionProduct.ProductId))
                            {
                                if (tier.RateType == CommisionRateTypes.FLAT_RATE) commission += tier.Value;
                                else if (tier.RateType == CommisionRateTypes.PERCENTAGE) commission += specificProduct.Product.Price * (tier.Value / 100);
                            }
                        }
                        else if (rule.RuleScope == CommisionRuleScopeTypes.MULTIPLES_OF_A_PRODUCT)
                        {
                            // multiples of a Product sold
                            foreach (var specificProduct in invoice.InvoiceProducts.Where(x => x.Product.Id == commisionProduct.ProductId))
                            {
                                var totalQty = specificProduct.Quantity;
                                if (tier.RateType == CommisionRateTypes.FLAT_RATE) commission += totalQty * tier.Value;
                                else if (tier.RateType == CommisionRateTypes.PERCENTAGE) commission += totalQty * specificProduct.Product.Price * (tier.Value / 100);
                            }

                        }
                    }
                }
            }
            return commission;
        }
    }
}