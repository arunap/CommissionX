using CommissionX.Core.Entities.Rules;
using MediatR;

namespace CommissionX.Application.Queries
{
    public class GetCommissionRuleByIdQuery : IRequest<CommissionRule>
    {
        public Guid Id { get; set; }

        public GetCommissionRuleByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}