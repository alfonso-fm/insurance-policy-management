using InsurancePolicyManagement.Application.Interfaces.Repositories;
using InsurancePolicyManagement.Domain.Entities;
using InsurancePolicyManagement.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicyManagement.Infrastructure.Persistence.Repositories;

public class ClientRepository : ICLientRepository
{
  private readonly InsuranceDBContext _context;
  public ClientRepository(InsuranceDBContext context)
  {
    _context = context;
  }
  public async Task AddAsync(Client client)
  {
     await _context.Clients.AddAsync(client);
  }

  public Task DeleteAsync(Client client)
  {
    _context.Clients.Remove(client);
    return Task.CompletedTask;
  }

  public async Task<bool> ExistsAsync(Guid clientId)
  {
    return await _context.Clients.AnyAsync(c => c.Id == clientId);
  }

  public async Task<IReadOnlyList<Client>> GetAllAsync()
  {
    return await _context.Clients
    .AsNoTracking()
    .ToListAsync();
  }

  public async Task<Client?> GetByIdAsync(Guid id)
  {
    return await _context.Clients.FindAsync(id);
  }

  public async Task<Client?> GetByNumberIdAsync(string numericId)
  {
    return await _context.Clients.FirstOrDefaultAsync(x => x.NumericId == numericId);
  }

  public async Task<IReadOnlyList<Client>> SearchAsync(string? name, string? email, string? numericId)
  {
    IQueryable<Client> query = _context.Clients.AsQueryable();
    
    if (!string.IsNullOrWhiteSpace(name))
    {
      query = query.Where(x => x.Name.Contains(name));
    }
    
    if (!string.IsNullOrWhiteSpace(email))
    {
      query = query.Where(x => x.Email.Contains(email));
    }
    
    if (!string.IsNullOrWhiteSpace(numericId))
    {
      query = query.Where(x => x.NumericId.Contains(numericId));
    }
    
    return await query.AsNoTracking().ToListAsync();
  }

  public Task UpdateAsync(Client client)
  {
    _context.Clients.Update(client);
    return Task.CompletedTask;
  }
}