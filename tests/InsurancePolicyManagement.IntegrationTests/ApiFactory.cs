using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using InsurancePolicyManagement.Infrastructure.Persistence.Context;

namespace InsurancePolicyManagement.IntegrationTests;
public class ApiFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            // Remove real DbContext
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<InsuranceDBContext>)
            );

            if (descriptor != null)
                services.Remove(descriptor);

            // Register InMemory DB
            services.AddDbContext<InsuranceDBContext>(options =>
            {
                options.UseInMemoryDatabase("IntegrationTestsDb");
            });

            // Build service provider
            var sp = services.BuildServiceProvider();

            // Seed data
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<InsuranceDBContext>();

            db.Database.EnsureCreated();

            TestDataSeeder.Seed(db);
        });
    }
}
