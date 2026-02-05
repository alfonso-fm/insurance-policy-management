using System.Text.Json;
using InsurancePolicyManagement.Application.DTOs;
using InsurancePolicyManagement.Application.Interfaces;
using InsurancePolicyManagement.Application.Interfaces.Repositories;

public class PolicyService : IPolicyService
{
  private readonly IPolicyRepository _repository;

  public PolicyService(IPolicyRepository repository)
  {
      _repository = repository;
  }

  public async Task CancelPolicyAsync(Guid policyId)
  {
    var policy = await _repository.GetByIdAsync(policyId)
        ?? throw new Exception("Client not found");

    policy.Status =  InsurancePolicyManagement.Domain.Enums.PolicyStatus.Cancelled;

    await _repository.UpdateAsync(policy);
  }

  public async Task<Guid> CreatePolicyAsync(CreatePolicyDto request)
  {
    //throw new NotImplementedException();
    return new Guid();
  }

  public async Task DeletePolicyAsync(Guid id)
  {
    //throw new NotImplementedException();
  }

  public async Task<IEnumerable<PolicyDto>> GetAllAsync()
  {
    Console.Write("¡YA LLEGO!");
    var policies = await _repository.GetAllAsync();

    var data = policies.Select(c => new PolicyDto
      {
        ClientId = c.ClientId,
        Type = c.Type,
        ValidityStartDate = c.ValidityStartDate,
        ValidityEndDate = c.ValidityEndDate,
        Amount = c.Amount,
        Status = c.Status  
      }
    );

        Console.WriteLine( 
        JsonSerializer.Serialize(data, new JsonSerializerOptions
        {
          WriteIndented = true
        })
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

  public async Task UpdatePolicyAsync(Guid policyId, UpdatePolicyDto request)
  {

    //throw new NotImplementedException();
  }
}