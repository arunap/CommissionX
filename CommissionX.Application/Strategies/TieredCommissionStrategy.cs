using CommissionX.Core.Entities;
using CommissionX.Core.Entities.Comissions;
using CommissionX.Core.Enums;
using CommissionX.Core.Interfaces;

namespace CommissionX.Application.Strategies
{
    public class TieredCommissionStrategy : ICommissionRule
    {
        private readonly List<TireCommisionRule> _rules;
        public TieredCommissionStrategy(List<TireCommisionRule> tiers)
        {
            _rules = tiers;
        }

        public decimal CalculateCommission(Invoice invoice, SalesPerson salesPerson)
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
                        if (rule.RuleScope == RuleScopeTypes.INVOICE)
                        {
                            if (tier.RateType == RateTypes.FLAT_RATE)
                            {
                                commission += tier.Value;
                            }
                            else if (tier.RateType == RateTypes.PERCENTAGE)
                            {
                                commission += invoice.TotalAmount * tier.Value / 100;
                            }
                        }
                        // check whether the sold quantity is in the range
                        else if (rule.RuleScope == RuleScopeTypes.PRODUCT)
                        {
                            foreach (var specificProduct in invoice.InvoiceProducts.Where(x => x.Product.Id == commisionProduct.ProductId))
                            {
                                if (tier.RateType == RateTypes.FLAT_RATE) commission += tier.Value;
                                else if (tier.RateType == RateTypes.PERCENTAGE) commission += specificProduct.Product.Price * (tier.Value / 100);
                            }
                        }
                        else if (rule.RuleScope == RuleScopeTypes.MULTIPLES_OF_A_PRODUCT)
                        {
                            // multiples of a Product sold
                            foreach (var specificProduct in invoice.InvoiceProducts.Where(x => x.Product.Id == commisionProduct.ProductId))
                            {
                                var totalQty = specificProduct.Quantity;
                                if (tier.RateType == RateTypes.FLAT_RATE) commission += totalQty * tier.Value;
                                else if (tier.RateType == RateTypes.PERCENTAGE) commission += totalQty * specificProduct.Product.Price * (tier.Value / 100);
                            }

                        }
                    }
                }
            }
            return commission;
        }
    }
}