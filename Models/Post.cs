namespace BlogApi.Models;

public class Post
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public DateTime CreationDate { get; set; }
    public IList<Comment> Comments { get; set; } = new List<Comment>();
}