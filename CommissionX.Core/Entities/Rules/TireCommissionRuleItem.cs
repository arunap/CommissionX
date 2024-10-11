using CommissionX.Core.Entities.Common;
using CommissionX.Core.Enums;

namespace CommissionX.Core.Entities.Rules
{
    public class TireCommissionRuleItem : BaseEntity
    {
        public int? TierStart { get; set; }
        public int? TierEnd { get; set; }
        public decimal Value { get; set; }
        public RateCalculationType RateCalculationType { get; set; } = RateCalculationType.None;
        public RuleContextType RuleContextType { get; set; } = RuleContextType.None;

        // navigation property
        public Guid CommissionRuleId { get; set; }
        public CommissionRule CommissionRule { get; set; }

        public bool IsInRange(int quantity)
        {
            if (quantity <= 0) return false;

            if (quantity >= TierStart && quantity <= TierEnd) return true;

            if (quantity >= TierStart && !TierEnd.HasValue) return true;

            return false;
        }
    }
}