using CommissionX.Core.Entities;

namespace CommissionX.Core.Interfaces
{
    public interface ICommissionAggregator
    {
        public void AddStrategry(ICommissionRule commissionRule);
        public decimal CalculateTotalCommission(Invoice invoice);
    }
}