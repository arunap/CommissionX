using CommissionX.Core.Entities.Common;
using CommissionX.Core.Entities.Rules;

namespace CommissionX.Core.Entities
{
    public class SalesPerson : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
        public ICollection<SalesPersonCommissionRule> SalesPersonCommissionRules { get; set; }
    }
}