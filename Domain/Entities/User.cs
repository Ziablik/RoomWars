namespace Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    
    public List<GameRoom> GameRooms { get; set; }
    public List<GameRoom> GameRoomsParticipation { get; set; }
    public List<GameStatistic> WinGames { get; set; }
    public List<GameStatistic> LoseGames { get; set; }
}