namespace Application.Dto;

public class GameRoomDto
{
    public Guid? Id { get; set; }
    public string RoomName { get; set; }
    public Guid HostId { get; set; }
    public int TotalUser { get; set; }
    public int CurrentUser { get; set; }
    public DateTime Created { get; set; }
}