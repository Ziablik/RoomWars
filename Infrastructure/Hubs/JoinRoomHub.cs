using Application.Common.Exceptions;
using Application.Contracts;
using Application.Dto;
using Domain.Enums;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Hubs;

public class JoinRoomHub  : Hub
{
    private readonly IGameRoomService _gameRoomService;
    private readonly IGameProcessingService _gameProcessingService;

    public JoinRoomHub(IGameRoomService gameRoomService, IGameProcessingService gameProcessingService)
    {
        _gameRoomService = gameRoomService;
        _gameProcessingService = gameProcessingService;
    }

    public async Task JoinRoom(JoinRoomDto joinRoomDto)
    {
        try
        {
            var gameRoom = await _gameRoomService.JoinRoom(joinRoomDto);
            await Groups.AddToGroupAsync(Context.ConnectionId, joinRoomDto.GameRoomId.ToString());
            await Clients.Caller.SendAsync("Notify", $"You are joined to the room {gameRoom.RoomName}");

            if (gameRoom.RoomStatus == RoomStatus.InStart)
            {
                await _gameProcessingService.InvokeGameProcess(gameRoom);
            }
        }
        catch (ValidationException)
        {
            await Clients.Caller.SendAsync("Error", "something went wrong during the game");
        }
        catch (Exception e)
        {
            await Clients.Caller.SendAsync("Error", $"bad request, error message: {e.Message}");
        }
    }
    
    public async Task LeaveRoom(string roomId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
        await Clients.Caller.SendAsync("Leave", "You are exit from room");
    }
}