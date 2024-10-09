using CommissionX.Core.Constants;

namespace CommissionX.Core.Entities.Comissions
{
    public sealed class TireCommisionRule : CommisionBase
    {
        public List<TireCommisionDto> Tires { get; set; }
    }

    public class TireCommisionDto
    {
        public int? TierStart { get; set; }
        public int? TierEnd { get; set; }
        public string RateType { get; set; } = CommisionRateTypes.FLAT_RATE;
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