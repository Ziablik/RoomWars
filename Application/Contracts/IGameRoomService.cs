using Application.Dto;
using Domain.Entities;

namespace Application.Contracts;

public interface IGameRoomService
{
    public Task<List<GameRoomDto>> GetList(CancellationToken cancellationToken);
    public Task<Guid> Add(GameRoomDto gameRoomDto, CancellationToken cancellationToken);
    public Task<GameRoom> JoinRoom(JoinRoomDto joinRoomDto);
    public Task CloseGame(GameRoom gameRoom);
}