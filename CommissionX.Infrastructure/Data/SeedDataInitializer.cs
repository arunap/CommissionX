using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommissionX.Core.Entities;
using CommissionX.Core.Entities.Comissions;
using CommissionX.Core.Interfaces;
using CommissionX.Infrastructure.Configurations;

namespace CommissionX.Infrastructure.Data
{
    public class SeedDataInitializer
    {
        private readonly ICommissionDataContext _context;

        public SeedDataInitializer(ICommissionDataContext context)
        {
            _context = context;
        }

        public async Task InitializeAsync()
        {
            var products = "Data/MockData/Products.json".SeedFromJson<Product>();
            var commisions = "Data/MockData/CommissionRules.json".SeedFromJson<CommissionRule>();
            var flats = "Data/MockData/FlatRules.json".SeedFromJson<FlatCommissionRule>();

            // var tired = "Data/MockData/TiredRules.json".SeedFromJson<TireCommisionRule>();
            //   var percents = "Data/MockData/PercentageRules.json".SeedFromJson<PercentageCommisionRule>();
            //  var caps = "Data/MockData/CapRules.json".SeedFromJson<CapCommissionRule>();

            _context.Products.AddRange(products);
            _context.CommissionRules.AddRange(commisions);
            _context.FlatCommissionRules.AddRange(flats);
            //  _context.TireCommissionRules.AddRange(tired);
            //   _context.PercentageCommissionRules.AddRange(percents);
            //   _context.CapCommissionRules.AddRange(caps);

            await _context.SaveChangesAsync();
        }
    }
}