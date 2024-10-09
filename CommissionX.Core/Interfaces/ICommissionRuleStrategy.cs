using CommissionX.Core.Entities;

namespace CommissionX.Core.Interfaces
{
    public interface ICommissionRuleStrategy
    {
        decimal CalculateCommission(SalesPerson salesPerson, Invoice invoice);
    }
}