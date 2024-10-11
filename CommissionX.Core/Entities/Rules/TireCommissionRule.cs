namespace CommissionX.Core.Entities.Rules
{
    [System.ComponentModel.DataAnnotations.Schema.NotMapped]
    public class TireCommissionRule : CommissionRule
    {
        public ICollection<TireCommissionRuleItem> Tires { get; set; }
    }
}