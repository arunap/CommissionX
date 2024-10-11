using CommissionX.Application.Strategies;
using CommissionX.Core.Entities;
using CommissionX.Core.Entities.Rules;
using CommissionX.Core.Enums;

namespace CommissionX.Tests.Application.Strategies
{
    public class FlatCommissionStrategyTests
    {
        [Fact]
        public void CalculateCommission_Should_Return_FlatCommission_For_Invoice_When_RuleContext_Is_Invoice()
        {
            // Arrange
            var commissionRules = new List<CommissionRule>
            {
                new CommissionRule
                {
                    RuleContextType = RuleContextType.Invoice,
                    Value = 200m
                }
            };

            var strategy = new FlatCommissionStrategy(commissionRules);

            var invoice = new Invoice { TotalAmount = 1000m };
            var salesPerson = new SalesPerson();

            // Act
            var result = strategy.CalculateCommission(invoice, salesPerson);

            // Assert
            Assert.Equal(200m, result); // Flat commission should be 200
        }

        [Fact]
        public void CalculateCommission_Should_Return_FlatCommission_For_Product_When_RuleContext_Is_Product()
        {
            // Arrange
            var commissionRules = new List<CommissionRule>
            {
                new CommissionRule
                {
                    RuleContextType = RuleContextType.Product,
                    ProductId = Guid.NewGuid(),
                    Value = 50m // Flat commission for the product
                }
            };

            var productId = commissionRules[0].ProductId.Value;
            var invoice = new Invoice
            {
                InvoiceProducts = new List<InvoiceProduct>
                {
                    new InvoiceProduct
                    {
                        ProductId = productId,
                        Product = new Product { Id = productId, Price = 500m }, // Price is not used in flat commission
                        Quantity = 1
                    }
                }
            };

            var strategy = new FlatCommissionStrategy(commissionRules);
            var salesPerson = new SalesPerson();

            // Act
            var result = strategy.CalculateCommission(invoice, salesPerson);

            // Assert
            Assert.Equal(50m, result); // Flat commission for the product should be 50
        }

        [Fact]
        public void CalculateCommission_Should_Return_FlatCommission_For_ProductMultiples_When_RuleContext_Is_ProductMultiples()
        {
            // Arrange
            var commissionRules = new List<CommissionRule>
            {
                new CommissionRule
                {
                    RuleContextType = RuleContextType.ProductMultiples,
                    ProductId = Guid.NewGuid(),
                    Value = 30m // Flat commission per product
                }
            };

            var productId = commissionRules[0].ProductId.Value;
            var invoice = new Invoice
            {
                InvoiceProducts = new List<InvoiceProduct>
                {
                    new InvoiceProduct
                    {
                        ProductId = productId,
                        Product = new Product { Id = productId, Price = 500m },
                        Quantity = 3 // Three units of this product
                    }
                }
            };

            var strategy = new FlatCommissionStrategy(commissionRules);
            var salesPerson = new SalesPerson();

            // Act
            var result = strategy.CalculateCommission(invoice, salesPerson);

            // Assert
            Assert.Equal(90m, result); // Flat commission for 3 products should be 3 * 30 = 90
        }

        [Fact]
        public void CalculateCommission_Should_Return_Zero_When_NoMatching_Product_InInvoice()
        {
            // Arrange
            var commissionRules = new List<CommissionRule>
            {
                new CommissionRule
                {
                    RuleContextType = RuleContextType.Product,
                    ProductId = Guid.NewGuid(), // Non-existent product
                    Value = 50m
                }
            };

            var invoice = new Invoice
            {
                InvoiceProducts = new List<InvoiceProduct>
                {
                    new InvoiceProduct
                    {
                        ProductId = Guid.NewGuid(), // Different product
                        Product = new Product { Id = Guid.NewGuid(), Price = 500m },
                        Quantity = 1
                    }
                }
            };

            var strategy = new FlatCommissionStrategy(commissionRules);
            var salesPerson = new SalesPerson();

            // Act
            var result = strategy.CalculateCommission(invoice, salesPerson);

            // Assert
            Assert.Equal(0m, result); // No matching product, so commission should be 0
        }

        [Fact]
        public void CalculateCommission_Should_CalculateCommission_For_Invoice_And_Products()
        {
            // Arrange
            var invoiceCommissionRule = new CommissionRule
            {
                RuleContextType = RuleContextType.Invoice,
                Value = 100m // Flat commission for the invoice
            };

            var productCommissionRule = new CommissionRule
            {
                RuleContextType = RuleContextType.Product,
                ProductId = Guid.NewGuid(),
                Value = 50m // Flat commission for a specific product
            };

            var productId = productCommissionRule.ProductId.Value;
            var invoice = new Invoice
            {
                TotalAmount = 1000m,
                InvoiceProducts = new List<InvoiceProduct>
                {
                    new InvoiceProduct
                    {
                        ProductId = productId,
                        Product = new Product { Id = productId, Price = 500m },
                        Quantity = 1
                    }
                }
            };

            var strategy = new FlatCommissionStrategy(new List<CommissionRule> { invoiceCommissionRule, productCommissionRule });
            var salesPerson = new SalesPerson();

            // Act
            var result = strategy.CalculateCommission(invoice, salesPerson);

            // Assert
            Assert.Equal(150m, result); // Total should be 100 (invoice) + 50 (product) = 150
        }
    }
}