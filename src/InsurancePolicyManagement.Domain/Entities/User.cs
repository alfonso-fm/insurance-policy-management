namespace InsurancePolicyManagement.Domain.Entities;

public class User
{
  public Guid Id { get; set; }
  public string Email { get; set; } = default!;
  public string PasswordHash { get; set; } = default!;

  public int RoleId { get; set; }
  public Role Role { get; set; } = default!;

  // Relación opcional con Client
  public Guid? ClientId { get; set; }
}