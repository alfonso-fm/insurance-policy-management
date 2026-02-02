using InsurancePolicyManagement.Domain.Enums;

namespace InsurancePolicyManagement.Application.DTOs;

public record CreatePolicyRequest(
    Guid ClientId,
    PolicyType Type,
    DateTime ValidityStartDate,
    DateTime ValidityEndDate,
    decimal Amount
);