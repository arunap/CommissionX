using CommissionX.Core.Constants;

namespace CommissionX.Core.Entities.Comissions
{
    public sealed class CapCommisionRule : CommisionBase
    {
        public decimal Value { get; set; }
        // navigation property
        public Guid CommissionRuleId { get; set; }
        public CommissionRule CommissionRule { get; set; }
        public string RateType { get; set; } = CommisionRateTypes.FLAT_RATE;
    }
}