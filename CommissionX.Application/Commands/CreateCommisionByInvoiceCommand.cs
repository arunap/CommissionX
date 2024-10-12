using CommissionX.Core.Interfaces;
using CommissionX.Application.Strategies;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CommissionX.Core.Enums;
using CommissionX.Core.Entities.Rules;

namespace CommissionX.Application.Commands
{
    public record InvoiceProductDto(Guid ProductId, int Quantity);
    public class CreateCommisionByInvoiceCommand : IRequest<decimal>
    {
        public Guid InvoiceId { get; set; }
        public Guid SalesPersonId { get; set; }
        public IEnumerable<InvoiceProductDto> InvoiceProducts { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class CreateCommisionByInvoiceCommandHandler : IRequestHandler<CreateCommisionByInvoiceCommand, decimal>
    {
        private readonly ICommissionAggregator _commissionAggregator;
        private readonly ICommissionDataContext _dataContext;

        public CreateCommisionByInvoiceCommandHandler(ICommissionAggregator commissionAggregator, ICommissionDataContext dataContext)
        {
            _commissionAggregator = commissionAggregator;
            _dataContext = dataContext;
        }

        public async Task<decimal> Handle(CreateCommisionByInvoiceCommand request, CancellationToken cancellationToken)
        {
            var rules = await _dataContext.CommissionRules
                .Include(x => x.SalesPersonCommissionRules)
                .Include(x => x.ProductCommissionRules)
                .ToListAsync();

            // flat commission rules
            var flatRules = rules.Where(c => c.CommissionRuleType == CommissionRuleType.Flat).ToList();
            ICommissionRule flatCommission = new FlatCommissionStrategy(flatRules);

            // percentage commission rules
            var percentageRules = rules.Where(c => c.CommissionRuleType == CommissionRuleType.Percentage).ToList();
            ICommissionRule percentageCommission = new PercentageCommissionStrategy(percentageRules);

            // tiered commission rules

            var tireCommissionRules = (from rule in rules
                                       join item in _dataContext.TireCommissionRuleItems on rule.Id equals item.CommissionRuleId
                                       group item by rule into grouped
                                       select new TireCommissionRule
                                       {
                                           Id = grouped.Key.Id,
                                           Name = grouped.Key.Name,
                                           RuleContextType = grouped.Key.RuleContextType,
                                           ProductCommissionRules = grouped.Key.ProductCommissionRules,
                                           SalesPersonCommissionRules = grouped.Key.SalesPersonCommissionRules,
                                           Tires = grouped.ToList()
                                       }).ToList();

            ICommissionRule tierdCommission = new TieredCommissionStrategy(tireCommissionRules);

            // prepare aggregator for all the applicable commisions
            _commissionAggregator.AddStrategry(flatCommission);
            _commissionAggregator.AddStrategry(percentageCommission);
            _commissionAggregator.AddStrategry(tierdCommission);

            // cap commision rule for total commission
            var capRule = rules.Where(c => c.CommissionRuleType == CommissionRuleType.Cap).FirstOrDefault() ?? new CommissionRule { };
            ICommissionRule capCommission = new CapCommissionStrategy(_commissionAggregator, capRule);

            var products = _dataContext.Products.Where(p => request.InvoiceProducts.Select(c => c.ProductId).Contains(p.Id)).ToList();
            var invoice = new Core.Entities.Invoice
            {
                Id = request.InvoiceId,
                TotalAmount = request.TotalAmount,
                SalesPersonId = request.SalesPersonId,
                InvoiceProducts = request.InvoiceProducts.Select(p => new Core.Entities.InvoiceProduct
                {
                    Product = products.First(c => c.Id == p.ProductId),
                    ProductId = p.ProductId,
                    Quantity = p.Quantity

                }).ToList(),
            };
            decimal totalCommission = capCommission.CalculateCommission(invoice, new Core.Entities.SalesPerson { });

            return totalCommission;
        }
    }
}