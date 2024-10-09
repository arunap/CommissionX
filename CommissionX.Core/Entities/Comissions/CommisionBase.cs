namespace CommissionX.Core.Entities.Comissions
{
    public abstract class CommisionBase : BaseEntity
    {
        public string RuleScope { get; set; } // Product, Invoice
        public Guid? ProductId { get; set; }
        public Product Product { get; set; }
    }
}