using CommissionX.Core.Entities.Common;
using CommissionX.Core.Enums;

namespace CommissionX.Core.Entities.Rules
{
    public class CommissionRule : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public decimal Value { get; set; } = 0.0m;
        public RuleContextType RuleContextType { get; set; } = RuleContextType.None; // Invoice, Product, ProductMultiples
        public RateCalculationType RateCalculationType { get; set; } = RateCalculationType.None; // Fixed, Percentage
        public CommissionRuleType CommissionRuleType { get; set; } = CommissionRuleType.None; // Flat, Percentage, Tier, Cap

        // navigation prop
        public Guid? ProductId { get; set; }
        public Product Product { get; set; }
        public ICollection<TireCommissionRuleItem> TireCommissionRuleItems { get; set; } = new List<TireCommissionRuleItem>();
        public ICollection<SalesPersonCommissionRule> SalesPersonCommissionRules { get; set; } = new List<SalesPersonCommissionRule>();
    }
}