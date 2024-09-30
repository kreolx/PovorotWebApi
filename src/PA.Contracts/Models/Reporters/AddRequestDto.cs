namespace PA.Contracts.Models.Reporters;

public record AddRequestDto(string CarModel, string Phone, string Description, DateTimeOffset RequestedAt);