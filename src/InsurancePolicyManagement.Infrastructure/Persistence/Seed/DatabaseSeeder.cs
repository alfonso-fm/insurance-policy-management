using InsurancePolicyManagement.Domain.Entities;
using InsurancePolicyManagement.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;

namespace InsurancePolicyManagement.Infrastructure.Persistence.Seed;

public class DatabaseSeeder
{
    private readonly InsuranceDBContext _context;
    private readonly ILogger<DatabaseSeeder> _logger;

    public DatabaseSeeder(
        InsuranceDBContext context,
        ILogger<DatabaseSeeder> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task SeedAsync()
    {
        _logger.LogInformation("Starting database seeding...");

        await SeedRolesAsync();
        await SeedAdminUserAsync();

        _logger.LogInformation("Database seeding completed.");
    }

    // ----------------------------
    // Roles
    // ----------------------------
    private async Task SeedRolesAsync()
    {
        if (await _context.Roles.AnyAsync())
            return;

        var roles = new List<Role>
        {
            new() { Name = "ADMIN" },
            new() { Name = "AGENT" },
            new() { Name = "CLIENT" }
        };

        _context.Roles.AddRange(roles);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Roles seeded.");
    }

    // ----------------------------
    // Admin User
    // ----------------------------
    private async Task SeedAdminUserAsync()
    {
        if (await _context.Users.AnyAsync(u => u.Email == "admin@insurance.com"))
            return;

        var adminRole = await _context.Roles.FirstAsync(r => r.Name == "ADMIN");

        var adminUser = new User
        {
            Id = Guid.NewGuid(),
            Email = "admin@insurance.com",
            PasswordHash = HashPassword("Admin123!"),
            RoleId = adminRole.Id
        };

        _context.Users.Add(adminUser);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Admin user seeded.");
    }

    // ----------------------------
    // Simple password hashing (demo)
    // ----------------------------
    private static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}
