namespace CommissionX.Application.DTOs
{
    public class InvoiceDto
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public IEnumerable<InvoiceItemDto> InvoiceItems { get; set; }
    }
}