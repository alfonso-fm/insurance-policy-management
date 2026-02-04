using InsurancePolicyManagement.Domain.Entities;

public interface IClientRepository
{
    Task<bool> ExistsByIdentificationAsync(string identification);
    Task<IEnumerable<Client>> GetAllAsync();
    Task<Client?> GetByIdAsync(Guid id);
    Task AddAsync(Client client);
    Task UpdateAsync(Client client);
    Task DeleteAsync(Client client);
    Task<bool> ExistsAsync(Guid clientId);
}