using CommissionX.Core.Entities.Rules;
using CommissionX.Core.Enums;
using CommissionX.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommissionX.Application.Queries
{
    public class GetCommissionRulesByTypeQuery : IRequest<List<CommissionRule>>
    {
        public CommissionRuleType CommissionRuleType;

        public GetCommissionRulesByTypeQuery(CommissionRuleType commissionRuleType)
        {
            CommissionRuleType = commissionRuleType;
        }

    }

    public class GetCommissionRulesByTypeQueryHandler : IRequestHandler<GetCommissionRulesByTypeQuery, List<CommissionRule>>
    {
        private readonly ICommissionDataContext _commissionDataContext;

        public GetCommissionRulesByTypeQueryHandler(ICommissionDataContext commissionDataContext)
        {
            _commissionDataContext = commissionDataContext;
        }

        public async Task<List<CommissionRule>> Handle(GetCommissionRulesByTypeQuery request, CancellationToken cancellationToken)
        {
            var rules = await _commissionDataContext
                .CommissionRules
                .Where(x => request.CommissionRuleType == CommissionRuleType.None || x.CommissionRuleType == request.CommissionRuleType)
                .ToListAsync();

            return rules;
        }
    }
}