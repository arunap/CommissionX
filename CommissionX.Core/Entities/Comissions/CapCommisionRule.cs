using CommissionX.Core.Enums;

namespace CommissionX.Core.Entities.Comissions
{
    public sealed class CapCommissionRule : CommisionBase
    {
        public decimal Value { get; set; }

        // navigation property
        public Guid CommissionRuleId { get; set; }
        public CommissionRule CommissionRule { get; set; }
        public RateTypes RateType { get; set; } = RateTypes.FLAT_RATE;
    }
}