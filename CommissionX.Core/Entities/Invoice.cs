namespace CommissionX.Core.Entities
{
    public class Invoice
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public SalesPerson SalesPerson { get; set; }
        public Guid SalesPersonId { get; set; }
        public ICollection<InvoiceProduct> InvoiceProducts { get; set; }
        public decimal TotalAmount { get; set; }
    }
}