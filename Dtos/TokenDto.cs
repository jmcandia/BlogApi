namespace BlogApi.Dtos;

public class TokenDto
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Token { get; set; } = null!;
}