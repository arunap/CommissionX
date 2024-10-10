namespace CommissionX.Core.Entities.Comissions
{
    public class CommissionRule : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        // navigation properties
        public ICollection<TireCommisionRule> TireCommissionRules { get; set; }
        public ICollection<FlatCommissionRule> FlatCommissionRules { get; set; }
        public ICollection<PercentageCommisionRule> PercentageCommissionRules { get; set; }
        public ICollection<CapCommissionRule> CapCommissionRules { get; set; }
    }
}