using InsurancePolicyManagement.Domain.Entities;
using InsurancePolicyManagement.Domain.Enums;

namespace InsurancePolicyManagement.UnitTests;
public class PolicyTests
{
  [Fact]
  public void Cancel_ShouldChangeStatusToCancelled()
  {
    var policy = new Policy(
      _clientId: Guid.NewGuid(),
      _type: PolicyType.Home,
      _validityStartDate: DateTime.UtcNow,
      _validityEndDate: DateTime.UtcNow.AddDays(1),
      _amount: 1000
    );
    policy.Status = PolicyStatus.Active;

    policy.Cancel();

    Assert.Equal(PolicyStatus.Cancelled, policy.Status);
  }
}
