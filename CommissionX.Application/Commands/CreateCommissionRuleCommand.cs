using CommissionX.Core.Entities;
using CommissionX.Core.Entities.Rules;
using CommissionX.Core.Enums;
using CommissionX.Core.Interfaces;
using MediatR;

namespace CommissionX.Application.Commands
{
    public class CreateCommissionRuleCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public decimal Value { get; set; } = 0.0m;
        public RuleContextType RuleContextType { get; set; }
        public RateCalculationType RateCalculationType { get; set; }
        public CommissionRuleType CommissionRuleType { get; set; }
        public List<Guid?> ProductIds { get; set; }
        public List<Guid?> PersonIds { get; set; }
    }

    public class CreateCommissionRuleCommandHandler : IRequestHandler<CreateCommissionRuleCommand, Guid>
    {
        private readonly ICommissionDataContext _context;

        public CreateCommissionRuleCommandHandler(ICommissionDataContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateCommissionRuleCommand request, CancellationToken cancellationToken)
        {
            var commisionRuleId = Guid.NewGuid();
            var commissionRule = new CommissionRule
            {
                Id = commisionRuleId,
                Name = request.Name,
                Value = request.Value,
                RuleContextType = request.RuleContextType,
                RateCalculationType = request.RateCalculationType,
                CommissionRuleType = request.CommissionRuleType,
                ProductCommissionRules = request.ProductIds.Select(x => new ProductCommissionRule
                {
                    ProductId = x.Value,
                    CommissionRuleId = commisionRuleId
                }).ToList(),
                SalesPersonCommissionRules = request.ProductIds.Select(x => new SalesPersonCommissionRule
                {
                    SalesPersonId = x.Value,
                    CommissionRuleId = commisionRuleId
                }).ToList()
            };

            _context.CommissionRules.Add(commissionRule);
            await _context.SaveChangesAsync(cancellationToken);

            return commissionRule.Id;
        }
    }
}