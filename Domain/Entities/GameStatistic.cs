namespace Domain.Entities;

public class GameStatistic
{
    public Guid GameRoomId { get; set; }
    public Guid WinnerId { get; set; }
    public Guid LoserId { get; set; }
    public int WinnerHealth { get; set; }
    public int RoundCount { get; set; }
    
    public GameRoom GameRoom { get; set; }
    public User Winner { get; set; }
    public User Loser { get; set; }
}