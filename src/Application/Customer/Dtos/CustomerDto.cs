namespace Application.Customer.Dtos;

public record CustomerDto(
    Guid Id, 
    string? FullName,
    string Email,
    string? Phone,
    string? Address);
