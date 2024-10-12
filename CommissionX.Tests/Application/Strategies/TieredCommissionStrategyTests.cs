using CommissionX.Application.Strategies;
using CommissionX.Core.Entities;
using CommissionX.Core.Entities.Rules;
using CommissionX.Core.Enums;

namespace CommissionX.Tests.Application.Strategies
{
    public class TieredCommissionStrategyTests
    {
        [Fact]
        public void CalculateCommission_Should_Return_Zero_When_NoMatching_Product_In_Invoice()
        {

            var tieredRules = new List<TireCommissionRule>
            {
                new TireCommissionRule
                {
                    ProductId = Guid.NewGuid(), // Non-existent product ID
                    Tires = new List<TireCommissionRuleItem>
                    {
                        new TireCommissionRuleItem
                        {
                            TierStart = 1,
                            TierEnd = 5,
                            RateCalculationType = RateCalculationType.Fixed,
                            RuleContextType = RuleContextType.Product,
                            Value = 50m
                        }
                    }
                }
            };

            var invoice = new Invoice
            {
                InvoiceProducts = new List<InvoiceProduct>
                {
                    new InvoiceProduct
                    {
                        ProductId = Guid.NewGuid(), // Different product ID
                        Quantity = 2,
                        Product = new Product { Price = 500m }
                    }
                }
            };

            var strategy = new TieredCommissionStrategy(tieredRules);
            var salesPerson = new SalesPerson();

            var result = strategy.CalculateCommission(invoice, salesPerson);

            Assert.Equal(0m, result); // No matching product, so commission should be 0
        }

        [Fact]
        public void CalculateCommission_Should_Return_Fixed_Commission_When_InRange_For_RuleContextType_Is_Invoice()
        {

            var productId = Guid.NewGuid();
            var tieredRules = new List<TireCommissionRule>
            {
                new TireCommissionRule
                {
                    ProductId = productId,
                    RuleContextType = RuleContextType.Invoice,
                    Tires = new List<TireCommissionRuleItem>
                    {
                        new TireCommissionRuleItem
                        {
                            TierStart = 1,
                            TierEnd = 10,
                            RateCalculationType = RateCalculationType.Fixed,
                            RuleContextType = RuleContextType.Product,
                            Value = 100m
                        }
                    }
                }
            };

            var invoice = new Invoice
            {
                TotalAmount = 1000m,
                InvoiceProducts = new List<InvoiceProduct>
                {
                    new InvoiceProduct
                    {
                        ProductId = productId,
                        Quantity = 1,
                        Product = new Product { Price = 500m }
                    }
                }
            };

            var strategy = new TieredCommissionStrategy(tieredRules);
            var salesPerson = new SalesPerson();

            var result = strategy.CalculateCommission(invoice, salesPerson);

            Assert.Equal(100m, result); // Fixed commission of 100
        }

        [Fact]
        public void CalculateCommission_Should_Return_PercentageCommission_When_InRange_For_RuleContextType_Is_Invoice()
        {

            var productId = Guid.NewGuid();
            var tieredRules = new List<TireCommissionRule>
            {
                new TireCommissionRule
                {
                    ProductId = productId,
                    RuleContextType = RuleContextType.Invoice,
                    Tires = new List<TireCommissionRuleItem>
                    {
                        new TireCommissionRuleItem
                        {
                            TierStart = 1,
                            TierEnd = 10,
                            RateCalculationType = RateCalculationType.Percentage,
                            RuleContextType = RuleContextType.Product,
                            Value = 10m // 10% commission
                        }
                    }
                }
            };

            var invoice = new Invoice
            {
                TotalAmount = 1000m,
                InvoiceProducts = new List<InvoiceProduct>
                {
                    new InvoiceProduct
                    {
                        ProductId = productId,
                        Quantity = 1,
                        Product = new Product { Price = 500m }
                    }
                }
            };

            var strategy = new TieredCommissionStrategy(tieredRules);
            var salesPerson = new SalesPerson();

            var result = strategy.CalculateCommission(invoice, salesPerson);

            Assert.Equal(100m, result); // 10% of 1000
        }

        [Fact]
        public void CalculateCommission_Should_CalculateCommission_For_Products_When_InRange()
        {

            var productId = Guid.NewGuid();
            var tieredRules = new List<TireCommissionRule>
            {
                new TireCommissionRule
                {
                    ProductId = productId,
                    RuleContextType = RuleContextType.Product,
                    Tires = new List<TireCommissionRuleItem>
                    {
                        new TireCommissionRuleItem
                        {
                            TierStart = 1,
                            TierEnd = 5,
                            RateCalculationType = RateCalculationType.Fixed,
                            Value = 20m
                        }
                    }
                }
            };

            var invoice = new Invoice
            {
                InvoiceProducts = new List<InvoiceProduct>
                {
                    new InvoiceProduct
                    {
                        ProductId = productId,
                        Quantity = 3,
                        Product = new Product { Price = 100m }
                    }
                }
            };

            var strategy = new TieredCommissionStrategy(tieredRules);
            var salesPerson = new SalesPerson();

            var result = strategy.CalculateCommission(invoice, salesPerson);

            Assert.Equal(20m, result); // Fixed commission of 20
        }

        [Fact]
        public void CalculateCommission_Should_Calculate_MultipleProduct_Commissions_When_InRange()
        {

            var productId = Guid.NewGuid();
            var tieredRules = new List<TireCommissionRule>
            {
                new TireCommissionRule
                {
                    ProductId = productId,
                    RuleContextType = RuleContextType.ProductMultiples,
                    Tires = new List<TireCommissionRuleItem>
                    {
                        new TireCommissionRuleItem
                        {
                            TierStart = 1,
                            TierEnd = 5,
                            RateCalculationType = RateCalculationType.Fixed,
                            Value = 10m // 10 per unit
                        }
                    }
                }
            };

            var invoice = new Invoice
            {
                InvoiceProducts = new List<InvoiceProduct>
                {
                    new InvoiceProduct
                    {
                        ProductId = productId,
                        Quantity = 3,
                        Product = new Product { Price = 100m }
                    }
                }
            };

            var strategy = new TieredCommissionStrategy(tieredRules);
            var salesPerson = new SalesPerson();

            var result = strategy.CalculateCommission(invoice, salesPerson);

            Assert.Equal(30m, result); // 3 products x 10 = 30
        }

        [Fact]
        public void CalculateCommission_Should_Calculate_PercentageCommission_For_ProductMultiples_When_InRange()
        {

            var productId = Guid.NewGuid();
            var tieredRules = new List<TireCommissionRule>
            {
                new TireCommissionRule
                {
                    ProductId = productId,
                    RuleContextType = RuleContextType.ProductMultiples,
                    Tires = new List<TireCommissionRuleItem>
                    {
                        new TireCommissionRuleItem
                        {
                            TierStart = 1,
                            TierEnd = 5,
                            RateCalculationType = RateCalculationType.Percentage,
                            Value = 20m // 20% commission
                        }
                    }
                }
            };

            var invoice = new Invoice
            {
                InvoiceProducts = new List<InvoiceProduct>
                {
                    new InvoiceProduct
                    {
                        ProductId = productId,
                        Quantity = 3,
                        Product = new Product { Price = 100m }
                    }
                }
            };

            var strategy = new TieredCommissionStrategy(tieredRules);
            var salesPerson = new SalesPerson();

            var result = strategy.CalculateCommission(invoice, salesPerson);

            Assert.Equal(60m, result); // 3 products x 100 (price) x 20% = 60
        }
    }

}