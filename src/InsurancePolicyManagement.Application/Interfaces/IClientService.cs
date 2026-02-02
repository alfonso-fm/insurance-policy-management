using InsurancePolicyManagement.Application.DTOs;

namespace InsurancePolicyManagement.Application.Interfaces;

public interface IClientService
{
  Task<Guid> CreateClientAsync(CreateClientRequest request);
  Task UpdateClientAsync(Guid clientId, UpdateClientRequest request);
}