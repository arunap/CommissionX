using CommissionX.Core.Entities.Common;

namespace CommissionX.Core.Entities
{
    public class SalesPerson : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
        public ICollection<SalesPersonCommissionRule> SalesPersonCommissionRules { get; set; }
    }
}