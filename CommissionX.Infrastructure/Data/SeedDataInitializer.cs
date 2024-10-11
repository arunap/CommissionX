using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using CommissionX.Core.Entities;
using CommissionX.Core.Entities.Rules;
using CommissionX.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            if (!await _context.Products.AnyAsync())
            {
                var products = SeedFromJson<Product>("Data/MockData/Products.json");
                _context.Products.AddRange(products);
                await _context.SaveChangesAsync();
            }

            if (!await _context.CommissionRules.AnyAsync())
            {
                var commisions = SeedFromJson<CommissionRule>("Data/MockData/CommissionRules.json");
                _context.CommissionRules.AddRange(commisions);
                await _context.SaveChangesAsync();
            }

            if (!await _context.TireCommisionRuleItems.AnyAsync())
            {
                var commisions = SeedFromJson<TireCommissionRule>("Data/MockData/TireCommissionRules.json");
                _context.CommissionRules.AddRange(commisions);
                await _context.SaveChangesAsync();
            }
        }

        public static List<T> SeedFromJson<T>(string jsonFilePath) where T : class
        {
            var entryPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var path = Path.Combine(entryPath, jsonFilePath);
            var jsonData = File.ReadAllText(path);

            var options = new JsonSerializerOptions { Converters = { new JsonStringEnumConverter() }, };
            var entities = JsonSerializer.Deserialize<List<T>>(jsonData, options);

            return entities;
        }
    }
}