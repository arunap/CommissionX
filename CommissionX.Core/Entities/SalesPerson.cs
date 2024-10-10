using CommissionX.Core.Entities.Comissions;

namespace CommissionX.Core.Entities
{
    public class SalesPerson
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
        public ICollection<CommissionRule> CommissionRules { get; set; }
    }
}