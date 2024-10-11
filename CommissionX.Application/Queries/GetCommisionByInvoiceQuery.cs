using CommissionX.Application.DTOs;
using CommissionX.Core.Interfaces;
using CommissionX.Application.Strategies;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CommissionX.Core.Enums;
using CommissionX.Core.Entities.Rules;

namespace CommissionX.Application.Queries
{
    // query
    public class GetCommisionByInvoiceQuery : IRequest<decimal>
    {
        public Guid PersonId { get; set; }
        public InvoiceDto Invoice { get; set; }

    }

    // query handler
    public class GetCommisionByInvoiceQueryHandler : IRequestHandler<GetCommisionByInvoiceQuery, decimal>
    {
        private readonly ICommissionAggregator _commissionAggregator;
        private readonly ICommissionDataContext _dataContext;

        public GetCommisionByInvoiceQueryHandler(ICommissionAggregator commissionAggregator, ICommissionDataContext dataContext)
        {
            _commissionAggregator = commissionAggregator;
            _dataContext = dataContext;
        }

        public async Task<decimal> Handle(GetCommisionByInvoiceQuery request, CancellationToken cancellationToken)
        {
            // flat commission rules
            var flatRules = await _dataContext.CommissionRules.Where(c => c.CommissionRuleType == CommissionRuleType.Flat).ToListAsync();
            ICommissionRule flatCommission = new FlatCommissionStrategy(flatRules);

            // percentage commission rules
            var percentageRules = await _dataContext.CommissionRules.Where(c => c.CommissionRuleType == CommissionRuleType.Percentage).ToListAsync();
            ICommissionRule percentageCommission = new PercentageCommissionStrategy(percentageRules);

            // tiered commission rules
            var tireCommissionRules = await _dataContext.CommissionRules
                .Where(c => c.CommissionRuleType == CommissionRuleType.Tier)
                .OfType<TireCommissionRule>()
                .Include(t => t.Tires)
                .ToListAsync();

            ICommissionRule tierdCommission = new TieredCommissionStrategy(tireCommissionRules);

            // prepare aggregator for all the applicable commisions
            _commissionAggregator.AddStrategry(flatCommission);
            _commissionAggregator.AddStrategry(percentageCommission);
            _commissionAggregator.AddStrategry(tierdCommission);

            // cap commision rule for total commission
            var capRule = await _dataContext.CommissionRules.Where(c => c.CommissionRuleType == CommissionRuleType.Cap).FirstOrDefaultAsync();
            ICommissionRule capCommission = new CapCommissionStrategy(_commissionAggregator, capRule);

            decimal totalCommission = capCommission.CalculateCommission(new Core.Entities.Invoice() { }, new Core.Entities.SalesPerson { });

            return totalCommission;
        }
    }
}