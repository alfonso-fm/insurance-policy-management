using InsurancePolicyManagement.Application.DTOs;

namespace InsurancePolicyManagement.Application.Interfaces;

public interface IPolicyService
{
    Task<Guid> CreatePolicyAsync(CreatePolicyRequest request);
    Task CancelPolicyAsync(Guid policyId);
}