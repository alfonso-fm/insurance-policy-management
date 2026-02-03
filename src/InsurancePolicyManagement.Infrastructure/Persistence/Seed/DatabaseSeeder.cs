using InsurancePolicyManagement.Application.Security;
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
        await SeedClientUserAsync();
        await SeedPoliciesAsync();

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

    private async Task SeedClientUserAsync()
    {
        if (await _context.Users.AnyAsync(u => u.Email == "client@insurance.com"))
            return;

        var clientRole = await _context.Roles.FirstAsync(r => r.Name == "CLIENT");

        var client = new Client
        {
            Id = Guid.NewGuid(),
            Name = "John Doe",
            Email = "client@insurance.com",
            Phone = "5551234567",
            NumericId = "1234567890"
        };

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = "client@insurance.com",
            PasswordHash = PasswordHasher.Hash("Client123!"),
            RoleId = clientRole.Id,
            ClientId = client.Id
        };

        _context.Clients.Add(client);
        _context.Users.Add(user);

        await _context.SaveChangesAsync();
    }

    private async Task SeedPoliciesAsync()
    {
        var client = await _context.Clients
            .FirstAsync(c => c.Email == "client@insurance.com");

        if (await _context.Policies.AnyAsync(p => p.ClientId == client.Id))
            return;

        var policies = new List<Policy>
        {
            new Policy(
                client.Id,
                Domain.Enums.PolicyType.Auto,
                DateTime.UtcNow,
                DateTime.UtcNow.AddYears(1),
                50000
            ),
            new Policy(
                client.Id,
                Domain.Enums.PolicyType.Life,
                DateTime.UtcNow,
                DateTime.UtcNow.AddYears(1),
                120000
            )
        };

        _context.Policies.AddRange(policies);
        await _context.SaveChangesAsync();
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
