
using InsurancePolicyManagement.Application.DTOs;
using InsurancePolicyManagement.Application.Exceptions;
using InsurancePolicyManagement.Application.Interfaces;
using InsurancePolicyManagement.Application.Interfaces.Repositories;
using InsurancePolicyManagement.Domain.Entities;

namespace InsurancePolicyManagement.Application.UseCases;

public class ClientService : IClientService
{
  private readonly ICLientRepository _clientRepository;
  private readonly IUnitOfWork _unitOfWork;

  public ClientService(ICLientRepository clientRepository, IUnitOfWork unitOfWork)
  {
    _clientRepository = clientRepository;
    _unitOfWork = unitOfWork;
  }
  public async Task<Guid> CreateClientAsync(CreateClientRequest request)
  {
    var client = new Client(
            request.NumericId,
            request.Name,
            request.Email,
            request.Phone
        );

        await _clientRepository.AddAsync(client);
        await _unitOfWork.SaveChangesAsync();

        return client.Id;
  }

  public async Task UpdateClientAsync(Guid clientId, UpdateClientRequest request)
  {
    var client = await _clientRepository.GetByIdAsync(clientId)
            ?? throw new NotFoundException("Client not found");

        client.UpdateContactInfo(request.Phone, request.Address);
        await _unitOfWork.SaveChangesAsync();
  }
}
