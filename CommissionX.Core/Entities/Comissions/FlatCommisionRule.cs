namespace CommissionX.Core.Entities.Comissions
{
    public sealed class FlatCommisionRule : CommisionBase
    {
        public decimal Value { get; set; }
        // navigation property
        public Guid CommissionRuleId { get; set; }
        public CommissionRule CommissionRule { get; set; }
    }

}