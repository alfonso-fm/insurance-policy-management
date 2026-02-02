using InsurancePolicyManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicyManagement.Infrastructure.Persistence.Context;
public class InsuranceDBContext: DbContext
{
  public InsuranceDBContext(DbContextOptions<InsuranceDBContext> options) : base(options)
  {
    
  }

  public DbSet<Client> Clients => Set<Client>();
  public DbSet<Policy> Policies => Set<Policy>();
  public DbSet<Role> Roles => Set<Role>();
  public DbSet<User> Users => Set<User>();
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(InsuranceDBContext).Assembly);
    base.OnModelCreating(modelBuilder);
  }
}