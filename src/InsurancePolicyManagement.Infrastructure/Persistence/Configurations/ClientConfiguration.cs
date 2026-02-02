using InsurancePolicyManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InsurancePolicyManagement.Infrastructure.Persistence.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
  public void Configure(EntityTypeBuilder<Client> builder)
  {
    builder.ToTable("Clients");

    builder.HasKey(x => x.Id);

    builder.Property(x => x.NumericId)
    .IsRequired()
    .HasMaxLength(10);

    builder.HasIndex(x => x.NumericId)
    .IsUnique();

    builder.Property(x => x.Name)
    .IsRequired()
    .HasMaxLength(150);

    builder.Property(x => x.Email)
    .IsRequired()
    .HasMaxLength(150);

    builder.Property(x => x.Phone)
    .IsRequired()
    .HasMaxLength(20);

    builder.Property(x => x.Address)
    .HasMaxLength(250);
  }
}