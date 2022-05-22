using Domain.Enums;

namespace Domain.Entities;

public class GameRoom
{
    public Guid Id { get; set; }
    public string RoomName { get; set; }
    public RoomStatus RoomStatus { get; set; }
    public int TotalUser { get; set; }
    public int CurrentTotalUser { get; set; }
    public Guid HostId { get; set; }
    public DateTime Created { get; set; }

    public GameStatistic Statistic { get; set; }
    public User Host { get; set; }
    public List<User> Users { get; set; }
}