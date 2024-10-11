namespace CommissionX.Core.Enums
{
    public enum RateCalculationType
    {
        None = 0,
        Fixed = 1,
        Percentage = 2
    }

    public enum RuleContextType
    {
        None = 0,
        Invoice = 1,
        Product = 2,
        ProductMultiples = 3,
    }

    public enum CommissionRuleType
    {
        None = 0,
        Flat = 1,
        Percentage = 2,
        Tier = 3,
        Cap = 4,
    }
}