using CommissionX.Core.Entities.Common;
using CommissionX.Core.Entities.Rules;

namespace CommissionX.Core.Entities
{
    public class SalesPersonCommissionRule : BaseEntity
    {
        public Guid CommissionRuleId { get; set; }
        public CommissionRule CommissionRule { get; set; }

        public Guid SalesPersonId { get; set; }
        public SalesPerson SalesPerson { get; set; }
    }
}