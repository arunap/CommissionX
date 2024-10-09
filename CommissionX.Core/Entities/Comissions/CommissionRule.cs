namespace CommissionX.Core.Entities.Comissions
{
    public sealed class CommissionRule : BaseEntity
    {
        public CommissionRule() => this.Id = Guid.NewGuid();

        // navigation properties
        public ICollection<TireCommisionRule> TireCommisionRules { get; set; }
        public ICollection<FlatCommisionRule> FlatCommisionRules { get; set; }
        public ICollection<PercentageCommisionRule> PercentageCommisionRules { get; set; }
        public ICollection<CapCommisionRule> CapCommisionRules { get; set; }
    }
}