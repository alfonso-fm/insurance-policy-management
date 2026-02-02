using InsurancePolicyManagement.Application.Interfaces;
using InsurancePolicyManagement.Infrastructure.Persistence.Context;

namespace InsurancePolicyManagement.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
  private readonly InsuranceDBContext _context;
  public UnitOfWork(InsuranceDBContext context)
  {
    _context = context;
  }

  public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    return _context.SaveChangesAsync(cancellationToken);
  }
}