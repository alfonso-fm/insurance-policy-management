namespace InsurancePolicyManagement.Application.DTOs;

public record UpdateClientRequest(
    string Phone,
    string Address
);