using CommissionX.Core.Entities.Rules;
using CommissionX.Core.Enums;
using CommissionX.Core.Exceptions;
using CommissionX.Core.Interfaces;
using MediatR;

namespace CommissionX.Application.Commands
{
    public class UpdateCommissionRuleCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public RuleContextType RuleContextType { get; set; }
        public RateCalculationType RateCalculationType { get; set; }
        public CommissionRuleType CommissionRuleType { get; set; }
        public Guid? ProductId { get; set; }
    }

    public class UpdateCommissionRuleCommandHandler : IRequestHandler<UpdateCommissionRuleCommand>
    {
        private readonly ICommissionDataContext _context;

        public UpdateCommissionRuleCommandHandler(ICommissionDataContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateCommissionRuleCommand request, CancellationToken cancellationToken)
        {
            var commissionRule = await _context.CommissionRules.FindAsync(request.Id) ?? throw new ItemNotFoundException(nameof(CommissionRule), request.Id);

            commissionRule.Name = request.Name;
            commissionRule.Value = request.Value;
            commissionRule.RuleContextType = request.RuleContextType;
            commissionRule.RateCalculationType = request.RateCalculationType;
            commissionRule.CommissionRuleType = request.CommissionRuleType;
            commissionRule.ProductId = request.ProductId;

            await _context.SaveChangesAsync(cancellationToken);

        }
    }
}