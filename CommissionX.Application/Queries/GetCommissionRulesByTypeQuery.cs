using CommissionX.Core.Entities.Comissions;
using CommissionX.Core.Enums;
using CommissionX.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommissionX.Application.Queries
{
    public class GetCommissionRulesByTypeQuery<TRule> : IRequest<List<TRule>> where TRule : class
    {
        public CommissionRuleTypes CommissionRuleType { get; set; }
    }

    public class GetCommissionRulesByTypeQueryHandler<TRule> : IRequestHandler<GetCommissionRulesByTypeQuery<TRule>, List<TRule>> where TRule : class
    {
        private readonly ICommissionDataContext _commissionDataContext;

        public GetCommissionRulesByTypeQueryHandler(ICommissionDataContext commissionDataContext)
        {
            _commissionDataContext = commissionDataContext;
        }

        public async Task<List<TRule>> Handle(GetCommissionRulesByTypeQuery<TRule> request, CancellationToken cancellationToken)
        {
            var rules = await _commissionDataContext.Set<TRule>().ToListAsync();
            return rules;
        }
    }
}