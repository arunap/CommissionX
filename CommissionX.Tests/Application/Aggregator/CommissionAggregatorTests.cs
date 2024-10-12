using CommissionX.Application.Strategies;
using CommissionX.Core.Entities;
using CommissionX.Core.Interfaces;
using Moq;

namespace CommissionX.Tests.Application.Aggregator
{
    public class CommissionAggregatorTests
    {
        [Fact]
        public void CalculateTotalCommission_With_NoStrategies_Returns_Zero()
        {

            var aggregator = new CommissionAggregator();
            var invoice = new Invoice { TotalAmount = 1000 }; // Sample invoice
            var salesPerson = new SalesPerson();

            // Act
            var result = aggregator.CalculateTotalCommission(invoice);

            // Assert
            Assert.Equal(0, result); // Total commission should be zero
        }

        [Fact]
        public void CalculateTotalCommission_With_SingleStrategy_Returns_Correct_Commission()
        {

            var mockCommissionRule = new Mock<ICommissionRule>();
            mockCommissionRule.Setup(r => r.CalculateCommission(It.IsAny<Invoice>())).Returns(150); // Mocked commission value

            var aggregator = new CommissionAggregator();
            aggregator.AddStrategry(mockCommissionRule.Object);

            var invoice = new Invoice { TotalAmount = 1000 };
            var salesPerson = new SalesPerson();

            // Act
            var result = aggregator.CalculateTotalCommission(invoice);

            // Assert
            Assert.Equal(150, result); // Should return the mocked commission value
        }

        [Fact]
        public void CalculateTotalCommission_With_Multiple_Strategies_Returns_SumOf_Commissions()
        {

            var mockCommissionRule1 = new Mock<ICommissionRule>();
            mockCommissionRule1.Setup(r => r.CalculateCommission(It.IsAny<Invoice>())).Returns(150);

            var mockCommissionRule2 = new Mock<ICommissionRule>();
            mockCommissionRule2.Setup(r => r.CalculateCommission(It.IsAny<Invoice>())).Returns(100);

            var aggregator = new CommissionAggregator();
            aggregator.AddStrategry(mockCommissionRule1.Object);
            aggregator.AddStrategry(mockCommissionRule2.Object);

            var invoice = new Invoice { TotalAmount = 1000 };
            var salesPerson = new SalesPerson();

            // Act
            var result = aggregator.CalculateTotalCommission(invoice);

            // Assert
            Assert.Equal(250, result); // Should return the sum of both mocked commission values
        }
    }
}