using InsurancePolicyManagement.Domain.Exceptions;
using InsurancePolicyManagement.Domain.Enums;

namespace InsurancePolicyManagement.Domain.Entities;

public class Policy
{
  public Guid Id { get;  set; }
  public Guid ClientId { get;  set; }

  public PolicyType Type { get;  set; }
  public PolicyStatus Status { get; set; }

  public DateTime ValidityStartDate { get; set; }
  public DateTime ValidityEndDate { get; set; }

  public decimal Amount { get; set; }
  public Policy() { } // EF
  
  public Policy(Guid _clientId, PolicyType _type, DateTime _validityStartDate, DateTime _validityEndDate, decimal _amount)
  {
    ValidateDates(_validityStartDate, _validityEndDate);
    ValidateAmount(_amount);

    Id = Guid.NewGuid();
    ClientId = _clientId;
    Type = _type;
    ValidityStartDate = _validityStartDate;
    ValidityEndDate = _validityEndDate;
    Amount = _amount;
    Status = PolicyStatus.Draft;
  }

  public void Activate()
  {
    if (Status != PolicyStatus.Draft){
      throw new DomainException("Only draft policies can be activated.");
    }
    Status = PolicyStatus.Active;
  }

  public void Cancel()
  {
    if (Status != PolicyStatus.Active){
      throw new DomainException("Only active policies can be cancelled.");
    }
    Status = PolicyStatus.Cancelled;
  }

  private void ValidateDates(DateTime start, DateTime end)
  {
    if (end <= start)
    {
      throw new DomainException("End date must be after start date.");
    }
  }

  private void ValidateAmount(decimal amount)
  {
    if (amount <= 0)
    {
      throw new DomainException("Insured amount must be greater than zero.");
    }
  }
}