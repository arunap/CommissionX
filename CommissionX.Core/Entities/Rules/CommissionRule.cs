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
        public Guid? ProductId { get; set; }

        // navigation prop
        public ICollection<TireCommissionRuleItem> TireCommissionRuleItems { get; set; }
    }
}