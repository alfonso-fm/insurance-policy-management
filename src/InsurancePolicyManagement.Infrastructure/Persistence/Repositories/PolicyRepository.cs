using InsurancePolicyManagement.Application.Interfaces.Repositories;
using InsurancePolicyManagement.Domain.Entities;
using InsurancePolicyManagement.Domain.Enums;
using InsurancePolicyManagement.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicyManagement.Infrastructure.Persistence.Repositories;

public class PolicyRepository : IPolicyRepository
{

  private readonly InsuranceDBContext _context;
  public PolicyRepository(InsuranceDBContext context)
  {
    _context = context;
  }

  public async Task AddAsync(Policy policy)
  {
    await _context.Policies.AddAsync(policy);
  }

  public async Task<IReadOnlyList<Policy>> FilterAsync(PolicyType? type, PolicyStatus? status, DateTime? validityStartDate, DateTime? validityEndDate)
  {
    IQueryable<Policy> query = _context.Policies.AsQueryable();
    
    if (type.HasValue)
    {
      query = query.Where(x => x.Type == type.Value);
    }
    
    if (status.HasValue)
    {
      query = query.Where(x => x.Status == status.Value);
    }
    
    if (validityStartDate.HasValue)
    {
      query = query.Where(x => x.ValidityStartDate >= validityStartDate.Value);
    }
    
    if (validityEndDate.HasValue)
    {
      query = query.Where(x => x.ValidityEndDate <= validityEndDate.Value);
    }
    
    return await query
      .AsNoTracking()
      .ToListAsync();
  }

  public async Task<IReadOnlyList<Policy>> GetByClientIdAsync(Guid clientId)
  {
    return await _context.Policies
      .Where(x => x.ClientId == clientId)
      .AsNoTracking()
      .ToListAsync();
  }

  public async Task<Policy?> GetByIdAsync(Guid id)
  {
    return await _context.Policies.FindAsync(id);
  }

  public Task UpdateAsync(Policy policy)
  {
    _context.Policies.Update(policy);
        return Task.CompletedTask;
  }
}