using System.Text.Json.Serialization;
using CommissionX.Core.Enums;

namespace CommissionX.Core.Entities.Comissions
{
    public abstract class CommisionBase : IBaseEntity
    {
        public Guid Id { get; set; }
        public RuleScopeTypes RuleScope { get; set; } // Product, Invoice
        public Guid? ProductId { get; set; }
        public Product Product { get; set; }
    }
}