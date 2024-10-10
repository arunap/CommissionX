namespace CommissionX.Application.DTOs
{
    public class InvoiceItemDto
    {
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}