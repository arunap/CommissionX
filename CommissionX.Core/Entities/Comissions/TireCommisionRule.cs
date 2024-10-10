using CommissionX.Core.Enums;

namespace CommissionX.Core.Entities.Comissions
{
    public sealed class TireCommisionRule : CommisionBase
    {
        public List<TireCommisionItem> Tires { get; set; }
    }

    public class TireCommisionItem : IBaseEntity
    {
        public Guid Id { get; set; }
        public int? TierStart { get; set; }
        public int? TierEnd { get; set; }
        public RateTypes RateType { get; set; } = RateTypes.FLAT_RATE;
        public decimal Value { get; set; }

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