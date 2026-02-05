using InsurancePolicyManagement.Application.DTOs;

namespace InsurancePolicyManagement.Application.Interfaces;

public interface IClientService
{
  Task<Guid> CreateClientAsync(CreateClientDto request);
  Task UpdateClientAsync(Guid clientId, UpdateClientDto request);
  Task<IEnumerable<ClientDto>> GetAllAsync();
  Task DeleteAsync(Guid id);
}