using CommissionX.Application.Strategies;
using CommissionX.Core.Entities;
using CommissionX.Core.Entities.Rules;
using CommissionX.Core.Enums;

namespace CommissionX.Tests.Application.Strategies
{
    public class PercentageCommissionStrategyTests
    {
        [Fact]
        public void CalculateCommission_Should_Return_Commission_BasedOn_InvoiceTotal_When_RuleContext_Is_Invoice()
        {

            var commissionRules = new List<CommissionRule>
            {
                new CommissionRule
                {
                    RuleContextType = RuleContextType.Invoice,
                    RateCalculationType = RateCalculationType.Percentage,
                    CommissionRuleType= CommissionRuleType.Percentage,
                    Value = 10m // 10% commission
                }
            };

            var strategy = new PercentageCommissionStrategy(commissionRules);

            // total invoice amount
            var invoice = new Invoice { TotalAmount = 100m };
            var salesPerson = new SalesPerson();

            var result = strategy.CalculateCommission(invoice, salesPerson);

            Assert.Equal(10m, result); // 10% of 100 should be 10
        }

        [Fact]
        public void CalculateCommission_Should_Return_CommissionFor_Products_When_RuleContext_Is_Product()
        {

            var commissionRules = new List<CommissionRule>
            {
                new CommissionRule
                {
                    ProductId = Guid.NewGuid(),
                    Value = 5m, // 5% commission for the product
                    RuleContextType = RuleContextType.Product,
                    RateCalculationType = RateCalculationType.Percentage,
                    CommissionRuleType= CommissionRuleType.Percentage,
                }
            };

            var product = new Product { Id = commissionRules[0].ProductId.Value, Price = 500m };

            var invoice = new Invoice
            {
                InvoiceProducts = new List<InvoiceProduct>
                {
                    new InvoiceProduct { ProductId = product.Id, Product = product, Quantity = 1 }
                }
            };

            var strategy = new PercentageCommissionStrategy(commissionRules);
            var salesPerson = new SalesPerson();

            var result = strategy.CalculateCommission(invoice, salesPerson);

            Assert.Equal(25m, result); // 5% of 500 should be 25
        }

        [Fact]
        public void CalculateCommission_Should_Return_Zero_When_NoMatching_Product_In_Invoice()
        {

            var commissionRules = new List<CommissionRule>
            {
                new CommissionRule
                {
                    Value = 5m,
                    ProductId = Guid.NewGuid(), // Non-existent product in invoice
                    RuleContextType = RuleContextType.Product,
                }
            };

            var invoice = new Invoice
            {
                InvoiceProducts = new List<InvoiceProduct>
                {
                    new InvoiceProduct
                    {
                        ProductId = Guid.NewGuid(), // Different product
                        Product = new Product { Price = 500m },
                        Quantity = 1
                    }
                }
            };

            var strategy = new PercentageCommissionStrategy(commissionRules);
            var salesPerson = new SalesPerson();

            var result = strategy.CalculateCommission(invoice, salesPerson);

            Assert.Equal(0m, result); // No matching product, so commission should be 0
        }

        [Fact]
        public void CalculateCommission_Should_CalculateCommission_For_Both_Invoice_And_Products()
        {

            var invoiceCommissionRule = new CommissionRule
            {
                RuleContextType = RuleContextType.Invoice,
                Value = 10m // 10% commission on total invoice
            };

            var productCommissionRule = new CommissionRule
            {
                RuleContextType = RuleContextType.Product,
                ProductId = Guid.NewGuid(),
                Value = 5m // 5% commission on a product
            };

            var product = new Product { Id = productCommissionRule.ProductId.Value, Price = 500m };

            var invoice = new Invoice
            {
                TotalAmount = 1000m,
                InvoiceProducts = new List<InvoiceProduct>
                {
                    new InvoiceProduct
                    {
                        ProductId = product.Id,
                        Product = product,
                        Quantity = 1
                    }
                }
            };

            var strategy = new PercentageCommissionStrategy(new List<CommissionRule> { invoiceCommissionRule, productCommissionRule });
            var salesPerson = new SalesPerson();

            var result = strategy.CalculateCommission(invoice, salesPerson);

            var expectedCommission = (1000m * 10m / 100) + (500m * 5m / 100);

            // Should calculate commission for both invoice and product
            Assert.Equal(expectedCommission, result);
        }
    }
}