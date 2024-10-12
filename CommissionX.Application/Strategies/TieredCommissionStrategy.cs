using CommissionX.Core.Entities;
using CommissionX.Core.Entities.Rules;
using CommissionX.Core.Enums;
using CommissionX.Core.Interfaces;

namespace CommissionX.Application.Strategies
{
    public class TieredCommissionStrategy : ICommissionRule
    {
        private readonly List<TireCommissionRule> _rules;
        public TieredCommissionStrategy(List<TireCommissionRule> rules)
        {
            _rules = rules;
        }

        public decimal CalculateCommission(Invoice invoice, SalesPerson salesPerson)
        {
            decimal commission = 0;

            foreach (var rule in _rules)
            {
                var commisionProduct = invoice.InvoiceProducts.FirstOrDefault(x => x.ProductId == rule.ProductId);
                if (commisionProduct == null) return 0;

                foreach (TireCommissionRuleItem tier in rule.Tires)
                {

                    if (tier.IsInRange(commisionProduct.Quantity))
                    {
                        // apply commision based on the total value of the invoice.
                        if (rule.RuleContextType == RuleContextType.Invoice)
                        {
                            if (tier.RateCalculationType == RateCalculationType.Fixed)
                            {
                                commission += tier.Value;
                            }
                            else if (tier.RateCalculationType == RateCalculationType.Percentage)
                            {
                                commission += invoice.TotalAmount * tier.Value / 100;
                            }
                        }
                        // check whether the sold quantity is in the range
                        else if (rule.RuleContextType == RuleContextType.Product)
                        {
                            foreach (var specificProduct in invoice.InvoiceProducts.Where(x => x.ProductId == commisionProduct.ProductId).ToList())
                            {
                                if (tier.RateCalculationType == RateCalculationType.Fixed) commission += tier.Value;
                                else if (tier.RateCalculationType == RateCalculationType.Percentage) commission += specificProduct.Product.Price * (tier.Value / 100);
                            }
                        }
                        else if (rule.RuleContextType == RuleContextType.ProductMultiples)
                        {
                            // multiples of a Product sold
                            foreach (var specificProduct in invoice.InvoiceProducts.Where(x => x.ProductId == commisionProduct.ProductId).ToList())
                            {
                                var totalQty = specificProduct.Quantity;
                                if (tier.RateCalculationType == RateCalculationType.Fixed) commission += totalQty * tier.Value;
                                else if (tier.RateCalculationType == RateCalculationType.Percentage) commission += totalQty * specificProduct.Product.Price * (tier.Value / 100);
                            }

                        }
                    }
                }
            }

            return commission;
        }
    }
}