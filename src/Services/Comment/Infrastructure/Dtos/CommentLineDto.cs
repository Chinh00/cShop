namespace Infrastructure.Dtos;

public class CommentLineDto
{
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public string Content { get; set; }
    public List<CommentLineDto> CommentLines { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}