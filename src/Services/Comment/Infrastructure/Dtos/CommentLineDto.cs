using Domain;

namespace Infrastructure.Dtos;

public class CommentLineDto
{
    public string Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public string Content { get; set; }
    public List<CommentLineDto> CommentLines { get; set; }
    public UserInfo User { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}