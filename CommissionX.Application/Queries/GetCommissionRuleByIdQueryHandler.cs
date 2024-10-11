using CommissionX.Core.Entities.Rules;
using CommissionX.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommissionX.Application.Queries
{
    public class GetCommissionRuleByIdQueryHandler : IRequestHandler<GetCommissionRuleByIdQuery, CommissionRule>
    {
        private readonly ICommissionDataContext _context;

        public GetCommissionRuleByIdQueryHandler(ICommissionDataContext context)
        {
            _context = context;
        }

        public async Task<CommissionRule> Handle(GetCommissionRuleByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.CommissionRules
                .FirstOrDefaultAsync(cr => cr.Id == request.Id, cancellationToken);
        }
    }

}