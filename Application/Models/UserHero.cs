namespace Application.Models;

public class UserHero
{
    public Guid UserId { get; }
    public string Username { get; }
    public int Health { get; set; } = 10;

    public UserHero(Guid userId, string username)
    {
        UserId = userId;
        Username = username;
    }
}