namespace Application.DTOs.Content;

public record ContentDto(int Id, string Title, string Description, string MediaUrl, int CreatorId, DateTime CreatedAt);
