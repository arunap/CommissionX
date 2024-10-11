using CommissionX.Application.Strategies;
using CommissionX.Core.Entities;
using CommissionX.Core.Entities.Rules;
using CommissionX.Core.Enums;
using CommissionX.Core.Interfaces;
using Moq;

namespace CommissionX.Tests.Application.Strategies
{
    public class CapCommissionStrategyTests
    {
        [Fact]
        public void CalculateCommission_TotalCommissionIsLessThanCap_ReturnsTotalCommission()
        {
            // Arrange
            var mockAggregator = new Mock<ICommissionAggregator>();
            var capRule = new CommissionRule
            {
                Id = Guid.NewGuid(),
                Name = "Cap Rule",
                Value = 100, // Fixed cap amount
                RateCalculationType = RateCalculationType.Fixed
            };

            var capCommissionStrategy = new CapCommissionStrategy(mockAggregator.Object, capRule);

            var invoice = new Invoice { TotalAmount = 500 };
            var salesPerson = new SalesPerson();

            mockAggregator.Setup(a => a.CalculateTotalCommission(invoice, salesPerson)).Returns(50);

            // Act
            var result = capCommissionStrategy.CalculateCommission(invoice, salesPerson);

            // Assert
            Assert.Equal(50, result);
        }

        [Fact]
        public void CalculateCommission_TotalCommissionExceedsCap_ReturnsCapAmount()
        {
            // Arrange
            var mockAggregator = new Mock<ICommissionAggregator>();
            var capRule = new CommissionRule
            {
                Id = Guid.NewGuid(),
                Name = "Cap Rule",
                Value = 100, // Fixed cap amount
                RateCalculationType = RateCalculationType.Fixed
            };

            var capCommissionStrategy = new CapCommissionStrategy(mockAggregator.Object, capRule);

            var invoice = new Invoice { TotalAmount = 500 };
            var salesPerson = new SalesPerson();

            mockAggregator.Setup(a => a.CalculateTotalCommission(invoice, salesPerson)).Returns(150);

            // Act
            var result = capCommissionStrategy.CalculateCommission(invoice, salesPerson);

            // Assert
            Assert.Equal(100, result); // The result should be capped at 100
        }

        [Fact]
        public void CalculateCommission_PercentageCapCalculation_ReturnsCapAmount()
        {
            // Arrange
            var mockAggregator = new Mock<ICommissionAggregator>();
            var capRule = new CommissionRule
            {
                Id = Guid.NewGuid(),
                Name = "Cap Rule",
                Value = 20, // Percentage cap amount
                RateCalculationType = RateCalculationType.Percentage
            };

            var capCommissionStrategy = new CapCommissionStrategy(mockAggregator.Object, capRule);

            var invoice = new Invoice { TotalAmount = 500 }; // 20% of 500 is 100
            var salesPerson = new SalesPerson();

            mockAggregator.Setup(a => a.CalculateTotalCommission(invoice, salesPerson)).Returns(50);

            // Act
            var result = capCommissionStrategy.CalculateCommission(invoice, salesPerson);

            // Assert
            Assert.Equal(50, result); // Should return the total commission since it's less than the cap

            mockAggregator.Setup(a => a.CalculateTotalCommission(invoice, salesPerson)).Returns(150);

            // Act
            result = capCommissionStrategy.CalculateCommission(invoice, salesPerson);

            // Assert
            Assert.Equal(100, result); // Should return the cap amount (100)
        }
    }
}