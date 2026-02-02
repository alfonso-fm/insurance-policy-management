using InsurancePolicyManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InsurancePolicyManagement.Infrastructure.Persistence.Configurations;

public class PolicyConfiguration : IEntityTypeConfiguration<Policy>
{
  public void Configure(EntityTypeBuilder<Policy> builder)
  {
    builder.ToTable("Policies");
    builder.HasKey(x => x.Id);
    
    builder.Property(x => x.Type)
    .HasConversion<int>()
    .IsRequired();
    
    builder.Property(x => x.Status)
    .HasConversion<int>()
    .IsRequired();
    
    builder.Property(x => x.ValidityStartDate)
    .IsRequired();
    
    builder.Property(x => x.ValidityEndDate)
    .IsRequired();
    
    builder.Property(x => x.Amount)
    .HasColumnType("decimal(18,2)")
    .IsRequired();
    
    builder.HasOne<Client>()
    .WithMany()
    .HasForeignKey(x => x.ClientId)
    .OnDelete(DeleteBehavior.Restrict);
  }
}
