using InsurancePolicyManagement.Domain.Entities;
using InsurancePolicyManagement.Domain.Enums;
using InsurancePolicyManagement.Application.Security;
using InsurancePolicyManagement.Infrastructure.Persistence.Context;

namespace InsurancePolicyManagement.IntegrationTests;

public static class TestDataSeeder
{
    public static void Seed(InsuranceDBContext context)
    {
        if (context.Users.Any())
            return;

        var adminRole = new Role
        {
            Id = 1,
            Name = "ADMIN"
        };

        context.Roles.Add(adminRole);

        var adminUser = new User
        {
            Id = Guid.NewGuid(),
            Email = "admin@insurance.com",
            PasswordHash = PasswordHasher.Hash("Admin123!"),
            RoleId = adminRole.Id
        };

        context.Users.Add(adminUser);

        context.SaveChanges();
    }
}
