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
        private readonly Mock<ICommissionAggregator> _aggregatorMock;
        private readonly List<CommissionRule> _capRules;
        private readonly CapCommissionStrategy _capCommissionStrategy;

        public CapCommissionStrategyTests()
        {
            _aggregatorMock = new Mock<ICommissionAggregator>();
            _capRules = new List<CommissionRule>();
            _capCommissionStrategy = new CapCommissionStrategy(_aggregatorMock.Object, _capRules);
        }

        [Fact]
        public void CalculateCommission_WhenNoCapRules_ShouldReturnTotalCommission()
        {
            var invoice = new Invoice { TotalAmount = 1000 };
            var salesPerson = new SalesPerson();
            _aggregatorMock.Setup(a => a.CalculateTotalCommission(invoice)).Returns(500);

            var result = _capCommissionStrategy.CalculateCommission(invoice);
            Assert.Equal(500, result);
        }

        [Fact]
        public void CalculateCommission_WhenCapRuleExists_ShouldApplyCap()
        {
            var invoice = new Invoice { TotalAmount = 1000 };
            var salesPerson = new SalesPerson();
            _capRules.Add(new CommissionRule { Value = 400, RateCalculationType = RateCalculationType.Fixed });

            _aggregatorMock.Setup(a => a.CalculateTotalCommission(invoice)).Returns(500);

            var result = _capCommissionStrategy.CalculateCommission(invoice);
            Assert.Equal(400, result);
        }

        [Fact]
        public void CalculateCommission_WhenMultipleCapRules_ShouldApplyMinimumCap()
        {
            var invoice = new Invoice { TotalAmount = 1000 };
            var salesPerson = new SalesPerson();
            _capRules.Add(new CommissionRule { Value = 600, RateCalculationType = RateCalculationType.Fixed });
            _capRules.Add(new CommissionRule { Value = 300, RateCalculationType = RateCalculationType.Fixed });

            _aggregatorMock.Setup(a => a.CalculateTotalCommission(invoice)).Returns(500);

            var result = _capCommissionStrategy.CalculateCommission(invoice);
            Assert.Equal(300, result);
        }

        [Fact]
        public void CalculateCommission_WhenTotalCommissionIsLessThanCap_ShouldReturnTotalCommission()
        {
            var invoice = new Invoice { TotalAmount = 1000 };
            var salesPerson = new SalesPerson();
            _capRules.Add(new CommissionRule { Value = 700, RateCalculationType = RateCalculationType.Fixed });

            _aggregatorMock.Setup(a => a.CalculateTotalCommission(invoice)).Returns(500);

            var result = _capCommissionStrategy.CalculateCommission(invoice);
            Assert.Equal(500, result);
        }

        [Fact]
        public void CalculateCommission_WhenCapRulesHavePercentage_ShouldApplyCapBasedOnPercentage()
        {
            var invoice = new Invoice { TotalAmount = 1000 };
            var salesPerson = new SalesPerson();
            _capRules.Add(new CommissionRule
            {
                Value = 50,
                RateCalculationType = RateCalculationType.Percentage,
                SalesPersonCommissionRules = new List<SalesPersonCommissionRule>()
            });

            _aggregatorMock.Setup(a => a.CalculateTotalCommission(invoice)).Returns(700);

            var result = _capCommissionStrategy.CalculateCommission(invoice);
            Assert.Equal(500, result);
        }

        [Fact]
        public void CalculateCommission_WhenNoSalesPersonCap_ShouldReturnGlobalCap()
        {
            var invoice = new Invoice { TotalAmount = 1000 };
            var salesPerson = new SalesPerson();
            _capRules.Add(new CommissionRule { Value = 400, RateCalculationType = RateCalculationType.Fixed });
            _capRules.Add(new CommissionRule { Value = 600, RateCalculationType = RateCalculationType.Fixed });

            _aggregatorMock.Setup(a => a.CalculateTotalCommission(invoice)).Returns(500);

            var result = _capCommissionStrategy.CalculateCommission(invoice);
            Assert.Equal(400, result);
        }

        [Fact]
        public void CalculateCommission_WhenMultipleRateCalculationTypes_ShouldApplyMinimumCap()
        {
            var invoice = new Invoice { TotalAmount = 1000 };
            var salesPerson = new SalesPerson();
            _capRules.Add(new CommissionRule { Value = 50, RateCalculationType = RateCalculationType.Percentage }); // 50%
            _capRules.Add(new CommissionRule { Value = 600, RateCalculationType = RateCalculationType.Fixed });

            _aggregatorMock.Setup(a => a.CalculateTotalCommission(invoice)).Returns(700);

            var result = _capCommissionStrategy.CalculateCommission(invoice);
            Assert.Equal(500, result);
        }
    }

}