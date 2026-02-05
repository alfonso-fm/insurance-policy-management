using InsurancePolicyManagement.Application.DTOs;

namespace InsurancePolicyManagement.Application.Interfaces;

public interface IPolicyService
{
    Task<Guid> CreatePolicyAsync(CreatePolicyDto request);
    Task CancelPolicyAsync(Guid policyId);

    Task UpdatePolicyAsync(Guid policyId, UpdatePolicyDto request);
    Task<IEnumerable<PolicyDto>> GetAllAsync();
    Task DeletePolicyAsync(Guid id);
    Task<IEnumerable<PolicyDto>> GetAllByClientIdAsync(Guid clientGuiId);
}