using CommissionX.Core.Entities;

namespace CommissionX.Core.Interfaces
{
    public interface ICommissionRule
    {
        decimal CalculateCommission(Invoice invoice);
    }
}