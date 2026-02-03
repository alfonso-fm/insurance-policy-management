using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using InsurancePolicyManagement.Infrastructure.Persistence.Context;
using Microsoft.Extensions.Configuration;

namespace InsurancePolicyManagement.IntegrationTests;
public class ApiFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        var dictionary = new Dictionary<string, string>
        {
            { "Jwt:Key", "TEST_SUPER_SECRET_KEY_1234567890123456" },
            { "Jwt:Issuer", "TestIssuer" },
            { "Jwt:Audience", "TestAudience" }
        };

        // Convert the Dictionary to the expected type
        IEnumerable<KeyValuePair<string, string?>> initialData = dictionary
            .Select(kvp => new KeyValuePair<string, string?>(kvp.Key, kvp.Value!)); // Use null-forgiving operator if values are guaranteed non-null

        builder.ConfigureAppConfiguration((context, config) =>{
            config.AddInMemoryCollection(initialData);
        });


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
