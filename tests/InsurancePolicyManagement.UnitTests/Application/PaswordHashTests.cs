using InsurancePolicyManagement.Application.Security;

namespace InsurancePolicyManagement.UnitTests;
public class PasswordHashTests
{
  [Fact]
  public void Hash_ShouldReturnSameValue_ForSameInput()
  {
    var h1 = PasswordHasher.Hash("Admin123!");
    var h2 = PasswordHasher.Hash("Admin123!");

    Assert.Equal(h1, h2);
  }
}