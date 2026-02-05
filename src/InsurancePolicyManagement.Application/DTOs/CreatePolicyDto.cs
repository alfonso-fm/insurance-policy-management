using InsurancePolicyManagement.Domain.Enums;

namespace InsurancePolicyManagement.Application.DTOs;

public record CreatePolicyDto(
    Guid ClientId,
    PolicyType Type,
    DateTime ValidityStartDate,
    DateTime ValidityEndDate,
    decimal Amount
);