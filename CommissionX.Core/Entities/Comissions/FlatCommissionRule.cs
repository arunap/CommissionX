namespace CommissionX.Core.Entities.Comissions
{
    public class FlatCommissionRule : CommisionBase
    {
        public decimal Value { get; set; }
        // navigation property
        public Guid CommissionRuleId { get; set; }
        public CommissionRule CommissionRule { get; set; }
    }

}