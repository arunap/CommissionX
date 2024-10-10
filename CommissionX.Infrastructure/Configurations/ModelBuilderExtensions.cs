using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace CommissionX.Infrastructure.Configurations
{
    public static class ModelBuilderExtensions
    {
        public static List<T> SeedFromJson<T>(this string jsonFilePath) where T : class
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