using InsurancePolicyManagement.Domain.Enums;

namespace InsurancePolicyManagement.Application.DTOs;

public class PolicyDto{
    public Guid ClientId { get; set; }
    public PolicyType Type { get; set; }
    public DateTime ValidityStartDate{ get; set; }
    public DateTime ValidityEndDate{ get; set; }
    public decimal Amount{ get; set; }
    public PolicyStatus Status{ get; set; }
}