using System.Text.Json;
using GasAwareness.API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GasAwareness.API.Seeding
{
    public class Seed
    {
        private readonly DataContext _context;
        private readonly IHostEnvironment _env;
        private readonly string _mainFilePath = "Seeding";

        public Seed(DataContext context, IHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task SeedData()
        {
            await SeedRoles();
            await SeedCategories();
            await SeedAgeGroups();
            await SeedSubscriptionTypes();
        }

        private async Task SeedRoles()
        {
            if (await _context.Roles.AnyAsync()) return;

            string filePath = Path.Combine(_env.ContentRootPath, _mainFilePath, "roles.json");

            if (File.Exists(filePath))
            {
                string jsonString = await File.ReadAllTextAsync(filePath);
                
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var roles = JsonSerializer.Deserialize<List<IdentityRole>>(jsonString, options);

                if (roles != null)
                {
                    foreach (var role in roles)
                    {
                        role.NormalizedName = role.Name.ToUpperInvariant();
                        await _context.Roles.AddAsync(role);
                    }
                    await _context.SaveChangesAsync();
                }
            }
        }

        private async Task SeedCategories()
        {
            if (await _context.Categories.AnyAsync()) return;

            string filePath = Path.Combine(_env.ContentRootPath, _mainFilePath, "categories.json");

            if (File.Exists(filePath))
            {
                string jsonString = await File.ReadAllTextAsync(filePath);
                
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var categories = JsonSerializer.Deserialize<List<Category>>(jsonString, options);

                if (categories != null)
                {
                    //categories.ForEach(x => x.CreatedAt = DateTime.UtcNow);
                    await _context.Categories.AddRangeAsync(categories);
                    await _context.SaveChangesAsync();
                }
            }
        }

        private async Task SeedAgeGroups()
        {
            if (await _context.AgeGroups.AnyAsync()) return;

            string filePath = Path.Combine(_env.ContentRootPath, _mainFilePath, "ageGroups.json");

            if (File.Exists(filePath))
            {
                string jsonString = await File.ReadAllTextAsync(filePath);
                
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var ageGroups = JsonSerializer.Deserialize<List<AgeGroup>>(jsonString, options);

                if (ageGroups != null)
                {
                    //ageGroups.ForEach(x => x.CreatedAt = DateTime.UtcNow);
                    await _context.AgeGroups.AddRangeAsync(ageGroups);
                    await _context.SaveChangesAsync();
                }
            }
        }

        private async Task SeedSubscriptionTypes()
        {
            if (await _context.SubscriptionTypes.AnyAsync()) return;

            string filePath = Path.Combine(_env.ContentRootPath, _mainFilePath, "subscriptionTypes.json");

            if (File.Exists(filePath))
            {
                string jsonString = await File.ReadAllTextAsync(filePath);
                
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var subscriptionTypes = JsonSerializer.Deserialize<List<SubscriptionType>>(jsonString, options);

                if (subscriptionTypes != null)
                {
                    //subscriptionTypes.ForEach(x => x.CreatedAt = DateTime.UtcNow);
                    await _context.SubscriptionTypes.AddRangeAsync(subscriptionTypes);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}