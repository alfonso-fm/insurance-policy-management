public record UpdatePolicyDto(
    DateTime ValidityStartDate,
    DateTime ValidityEndDate,
    decimal Amount,
    int Status
);