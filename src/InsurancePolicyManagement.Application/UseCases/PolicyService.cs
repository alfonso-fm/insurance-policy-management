using InsurancePolicyManagement.Application.DTOs;
using InsurancePolicyManagement.Application.Exceptions;
using InsurancePolicyManagement.Application.Interfaces;
using InsurancePolicyManagement.Application.Interfaces.Repositories;
using InsurancePolicyManagement.Domain.Entities;
using InsurancePolicyManagement.Domain.Exceptions;

namespace InsurancePolicyManagement.Application.UseCases;

public class PolicyService : IPolicyService
{

  private readonly IPolicyRepository _policyRepository;
  private readonly IClientRepository _clientRepository;
  private readonly IUnitOfWork _unitOfWork;

  public PolicyService(IPolicyRepository policyRepository, IClientRepository clientRepository, IUnitOfWork unitOfWork)
  {
    _policyRepository = policyRepository;
    _clientRepository = clientRepository;
    _unitOfWork = unitOfWork;
  }
  public async Task CancelPolicyAsync(Guid policyId)
  {
    var policy = await _policyRepository.GetByIdAsync(policyId) ?? throw new NotFoundException("Policy not found");

    policy.Cancel();
    await _unitOfWork.SaveChangesAsync();
}

  public async Task<Guid> CreatePolicyAsync(CreatePolicyRequest request)
  {
    var clientExists = await _clientRepository.ExistsAsync(request.ClientId);
    if (!clientExists)
    throw new DomainException("Client does not exist");

    var policy = new Policy(
      request.ClientId,
      request.Type,
      request.ValidityStartDate,
      request.ValidityEndDate,
      request.Amount
    );

    await _policyRepository.AddAsync(policy);
    await _unitOfWork.SaveChangesAsync();

    return policy.Id;
  }
}