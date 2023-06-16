namespace BlogApi.Dtos;

public class PostDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public UserDto? User { get; set; }
    public IList<CommentDto> Comments { get; set; } = new List<CommentDto>();
}