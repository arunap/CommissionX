using CommissionX.Core.Entities.Common;

namespace CommissionX.Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        // navigation prop
        public ICollection<InvoiceProduct> InvoiceProducts { get; set; }
        // public ICollection<ProductCommissionRule> ProductCommissionRules { get; set; }
    }
}