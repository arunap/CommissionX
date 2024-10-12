using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommissionX.Core.Entities.Common;
using CommissionX.Core.Entities.Rules;

namespace CommissionX.Core.Entities
{
    public class ProductCommissionRule : BaseEntity
    {
        public Guid CommissionRuleId { get; set; }
        public CommissionRule CommissionRule { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}