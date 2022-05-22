using Application.Common.Exceptions;
using Application.Contracts;
using Application.Dto;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class GameRoomService : IGameRoomService
{
    private readonly IRepository<GameRoom> _gameRoomRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IMapper _mapper;
    private const int TotalUsers = 2;

    public GameRoomService(IRepository<GameRoom> gameRoomRepository, IRepository<User> userRepository, IMapper mapper)
    {
        _gameRoomRepository = gameRoomRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<List<GameRoomDto>> GetList(CancellationToken cancellationToken)
    {
        var gameRooms = await _gameRoomRepository
            .FindByCondition(x => x.RoomStatus == RoomStatus.Open, false)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<GameRoomDto>>(gameRooms);
    }

    public async Task<Guid> Add(GameRoomDto gameRoomDto, CancellationToken cancellationToken)
    {
        var gameRoom = _mapper.Map<GameRoom>(gameRoomDto);
        gameRoom.TotalUser = TotalUsers;
        gameRoom.Created = DateTime.Now;

        await _gameRoomRepository.CreateAsync(gameRoom, cancellationToken);
        await _gameRoomRepository.SaveChangesAsync(cancellationToken);

        return gameRoom.Id;
    }

    public async Task<GameRoom> JoinRoom(JoinRoomDto joinRoomDto)
    {
        var gameRoom = await _gameRoomRepository
            .FindByCondition(x => x.Id == joinRoomDto.GameRoomId && x.RoomStatus == RoomStatus.Open, true)
            .Include(x => x.Users)
            .FirstOrDefaultAsync();

        if (gameRoom is null)
        {
            throw new NotFoundException("Game room not found");
        }

        if (gameRoom.HostId == joinRoomDto.UserId)
        {
            throw new ValidationException("You are host of the room");
        }

        if (gameRoom.Users.Count == TotalUsers)
        {
            throw new ValidationException("Room is already been started");
        }

        await AddUserToRoom(gameRoom, joinRoomDto.UserId);

        if (gameRoom.Users.Count == TotalUsers)
        {
            gameRoom.RoomStatus = RoomStatus.InStart;
        }
        
        await _gameRoomRepository.SaveChangesAsync();
        
        return gameRoom;
    }

    public async Task CloseGame(GameRoom gameRoom)
    {
        gameRoom.RoomStatus = RoomStatus.Closed;
        await _gameRoomRepository.SaveChangesAsync();
    }

    private async Task AddUserToRoom(GameRoom gameRoom, Guid userId)
    {
        var user = await _userRepository
            .FindByCondition(x => x.Id == userId, true)
            .FirstOrDefaultAsync();

        if (user is null)
        {
            throw new NotFoundException("User not found");
        }
        
        gameRoom.Users.Add(user);
        gameRoom.CurrentTotalUser++;
    }
}