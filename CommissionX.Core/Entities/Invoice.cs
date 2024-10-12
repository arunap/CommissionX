using CommissionX.Core.Entities.Common;

namespace CommissionX.Core.Entities
{
    public class Invoice: BaseEntity
    {
        public DateTime Date { get; set; }
        public Guid SalesPersonId { get; set; }
        public SalesPerson SalesPerson { get; set; }
        public ICollection<InvoiceProduct> InvoiceProducts { get; set; }
        public decimal TotalAmount { get; set; }
    }
}