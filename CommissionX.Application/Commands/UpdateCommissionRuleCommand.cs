using CommissionX.Core.Entities;
using CommissionX.Core.Entities.Rules;
using CommissionX.Core.Enums;
using CommissionX.Core.Exceptions;
using CommissionX.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
        public List<Guid?> ProductIds { get; set; }
        public List<Guid?> PersonIds { get; set; }
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

            var existingProductCommissionRules = await _context.ProductCommissionRules.Where(c => c.CommissionRuleId == request.Id && request.ProductIds.Contains(c.ProductId)).ToListAsync();
            _context.ProductCommissionRules.RemoveRange(existingProductCommissionRules);

            var newProductCommissionRules = request.ProductIds.Select(c => new ProductCommissionRule { CommissionRuleId = request.Id, ProductId = c.Value }).ToList();
            _context.ProductCommissionRules.AddRange(newProductCommissionRules);

            var existingSalesPersons = await _context.SalesPersonCommissionRules.Where(c => c.CommissionRuleId == request.Id && request.PersonIds.Contains(c.SalesPersonId)).ToListAsync();
            _context.SalesPersonCommissionRules.RemoveRange(existingSalesPersons);

            var newSalesPersonsList = request.ProductIds.Select(c => new SalesPersonCommissionRule { CommissionRuleId = request.Id, SalesPersonId = c.Value }).ToList();
            _context.SalesPersonCommissionRules.AddRange(newSalesPersonsList);

            await _context.SaveChangesAsync(cancellationToken);

        }
    }
}