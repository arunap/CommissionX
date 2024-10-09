namespace CommissionX.Core.Entities
{
    public class InvoiceProduct
    {
        public Guid InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}