using InsurancePolicyManagement.Application.DTOs;
using InsurancePolicyManagement.Application.Interfaces;
using InsurancePolicyManagement.Domain.Entities;

public class ClientService : IClientService
{
    private readonly IClientRepository _repository;

    public ClientService(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> CreateClientAsync(CreateClientDto dto)
    {
        if (await _repository.ExistsByIdentificationAsync(dto.NumericId))
            throw new Exception("Client already exists");

        var client = new Client
        {
            Id = Guid.NewGuid(),
            NumericId = dto.NumericId ,
            Name = dto.Name,
            Email = dto.Email,
            Phone = dto.Phone
        };

        await _repository.AddAsync(client);
        
        return client.Id;
    }

    public async Task<IEnumerable<ClientDto>> GetAllAsync()
    {
        var clients = await _repository.GetAllAsync();

        return clients.Select(c => new ClientDto
        {
            Id = c.Id,
            NumericId = c.NumericId,
            Name = c.Name,
            Email = c.Email,
            Phone = c.Phone,
            Address = c.Address,
        });
    }

    public async Task UpdateClientAsync(Guid id, UpdateClientDto dto)
    {
        var client = await _repository.GetByIdAsync(id)
            ?? throw new Exception("Client not found");

        client.Address = dto.Address;
        client.Phone = dto.Phone;

        await _repository.UpdateAsync(client);
    }

    public async Task DeleteAsync(Guid id)
    {
        var client = await _repository.GetByIdAsync(id)
            ?? throw new Exception("Client not found");

        await _repository.DeleteAsync(client);
    }
}
