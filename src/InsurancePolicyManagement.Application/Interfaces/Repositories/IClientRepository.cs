using InsurancePolicyManagement.Domain.Entities;

namespace InsurancePolicyManagement.Application.Interfaces.Repositories;

public interface ICLientRepository
{
  Task<Client?> GetByIdAsync(Guid id);
  Task<Client?> GetByNumberIdAsync(string numberId);
  Task<bool> ExistsAsync(Guid clientId);

  Task<IReadOnlyList<Client>> GetAllAsync();
  Task<IReadOnlyList<Client>> SearchAsync(string? name, string? email, string? numberId);

  Task AddAsync(Client client);
  Task UpdateAsync(Client client);
  Task DeleteAsync(Client client);
}