namespace InsurancePolicyManagement.Application.DTOs;

public record CreateClientRequest(
    string NumericId,
    string Name,
    string Email,
    string Phone,
    string Address
);
