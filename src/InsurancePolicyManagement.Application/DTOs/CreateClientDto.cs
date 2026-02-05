namespace InsurancePolicyManagement.Application.DTOs;

public record CreateClientDto(
    string NumericId,
    string Name,
    string Email,
    string Phone,
    string Address
);
