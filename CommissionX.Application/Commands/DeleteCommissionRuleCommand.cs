using CommissionX.Core.Entities.Rules;
using CommissionX.Core.Exceptions;
using CommissionX.Core.Interfaces;
using MediatR;

namespace CommissionX.Application.Commands
{
    public class DeleteCommissionRuleCommand : IRequest
    {
        public Guid Id { get; set; }

        public DeleteCommissionRuleCommand(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteCommissionRuleCommandHandler : IRequestHandler<DeleteCommissionRuleCommand>
    {
        private readonly ICommissionDataContext _context;

        public DeleteCommissionRuleCommandHandler(ICommissionDataContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteCommissionRuleCommand request, CancellationToken cancellationToken)
        {
            var commissionRule = await _context.CommissionRules.FindAsync(request.Id) ?? throw new ItemNotFoundException(nameof(CommissionRule), request.Id);

            _context.CommissionRules.Remove(commissionRule);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

}