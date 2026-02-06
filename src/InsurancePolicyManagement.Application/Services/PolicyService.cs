using System.Text.Json;
using InsurancePolicyManagement.Application.DTOs;
using InsurancePolicyManagement.Application.Interfaces;
using InsurancePolicyManagement.Application.Interfaces.Repositories;
using InsurancePolicyManagement.Domain.Entities;

public class PolicyService : IPolicyService
{
  private readonly IPolicyRepository _repository;

  public PolicyService(IPolicyRepository repository)
  {
      _repository = repository;
  }

  public async Task<PolicyDto> GetByIdAsync(Guid Id)
  {
    Console.Write("Llego al servicio!");
    var policy = await _repository.GetByIdAsync(Id);

    if (policy == null)
      throw new KeyNotFoundException($"Policy with id {Id} was not found.");

    var dto = new PolicyDto
    {
      Id = policy.Id,
      ClientId = policy.ClientId,
      Type = policy.Type,
      ValidityStartDate = policy.ValidityStartDate,
      ValidityEndDate = policy.ValidityEndDate,
      Amount = policy.Amount,
      Status = policy.Status
    };

    
    return dto;
  }

  public async Task CancelPolicyAsync(Guid policyId)
  {
    var policy = await _repository.GetByIdAsync(policyId)
        ?? throw new Exception("Client not found");

    policy.Status =  InsurancePolicyManagement.Domain.Enums.PolicyStatus.Cancelled;

    await _repository.UpdateAsync(policy);
  }

  public async Task<Guid> CreatePolicyAsync(CreatePolicyDto dto)
  {

    Console.Write("ya llegue!");
    var client = new Policy
    {
      Id = Guid.NewGuid(),
      Type = dto.Type ,
      ValidityEndDate = dto.ValidityEndDate,
      ValidityStartDate = dto.ValidityStartDate,
      Amount = dto.Amount,
      Status = InsurancePolicyManagement.Domain.Enums.PolicyStatus.Active,
      ClientId = dto.ClientId

    };

    await _repository.AddAsync(client);

    return client.Id;
  }

  public async Task DeletePolicyAsync(Guid id)
  {
    //throw new NotImplementedException();
  }

  public async Task<IEnumerable<PolicyDto>> GetAllAsync()
  {
    var policies = await _repository.GetAllAsync();

    var data = policies.Select(c => new PolicyDto
      {
        Id = c.Id,
        ClientId = c.ClientId,
        Type = c.Type,
        ValidityStartDate = c.ValidityStartDate,
        ValidityEndDate = c.ValidityEndDate,
        Amount = c.Amount,
        Status = c.Status  
      }
    );
    return data;
  }

  public async Task<IEnumerable<PolicyDto>> GetAllByClientIdAsync(Guid clientId)
  {
    var policies = await _repository.GetByClientIdAsync(clientId);
    return policies.Select(c => new PolicyDto
      {
        ClientId = c.ClientId,
        Type = c.Type,
        ValidityStartDate = c.ValidityStartDate,
        ValidityEndDate = c.ValidityEndDate,
        Amount = c.Amount,
        Status = c.Status  
      }
    );
  }


  public async Task UpdatePolicyAsync(Guid policyId, UpdatePolicyDto dto)
  {
    var policy = await _repository.GetByIdAsync(policyId)
      ?? throw new Exception("Client not found");

    policy.Amount = dto.Amount;
    policy.Status = (InsurancePolicyManagement.Domain.Enums.PolicyStatus)dto.Status;
    policy.ValidityStartDate= dto.ValidityStartDate;
    policy.ValidityEndDate= dto.ValidityEndDate;

    await _repository.UpdateAsync(policy);
  }
}