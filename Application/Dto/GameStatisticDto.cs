namespace Application.Dto;

public class GameStatisticDto
{
    public Guid GameRoomId { get; set; }
    public Guid WinnerId { get; set; }
    public Guid LoserId { get; set; }
    public int WinnerHealth { get; set; }
    public string WinnerUsername { get; set; }
    public string LoserUsername { get; set; }
    public string GameRoomName { get; set; }
}