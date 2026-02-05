using InsurancePolicyManagement.Domain.Entities;
using InsurancePolicyManagement.Domain.Enums;

namespace InsurancePolicyManagement.Application.Interfaces.Repositories;

public interface IPolicyRepository
{
  Task<Policy?> GetByIdAsync(Guid id);
  
  Task<IReadOnlyList<Policy>> GetByClientIdAsync(Guid clientId);

  Task<IReadOnlyList<Policy>> GetAllAsync();
  Task<IReadOnlyList<Policy>> FilterAsync(PolicyType? type, PolicyStatus? status, DateTime? validityStartDate, DateTime? validityEndDate);

  Task AddAsync(Policy policy);
  Task UpdateAsync(Policy policy);
}