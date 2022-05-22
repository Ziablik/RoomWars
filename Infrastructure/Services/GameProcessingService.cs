using Application.Common.Exceptions;
using Application.Contracts;
using Application.Dto;
using Application.Models;
using Domain.Entities;

namespace Infrastructure.Services;

public class GameProcessingService : IGameProcessingService
{
    private readonly IDamageDealerService _damageDealerService;
    private readonly IGameRoomService _gameRoomService;
    private readonly IHubMassagerService _hubMassager;
    private readonly IGameStatisticService _gameStatisticService;

    public GameProcessingService(
        IDamageDealerService damageDealerService, 
        IGameRoomService gameRoomService, 
        IHubMassagerService hubMassager, 
        IGameStatisticService gameStatisticService
    )
    {
        _damageDealerService = damageDealerService;
        _gameRoomService = gameRoomService;
        _hubMassager = hubMassager;
        _gameStatisticService = gameStatisticService;
    }

    public async Task InvokeGameProcess(GameRoom gameRoom)
    {
        var usersFromRoom = gameRoom.Users;
        if (usersFromRoom.Count != 2)
        {
            throw new ValidationException("Something error, should be 2 players in room");
        }
        
        await _hubMassager
            .SendGroupNotify(gameRoom.Id.ToString(), "Game Is Start");

        var userHeroes = await StartGame(usersFromRoom, gameRoom.Id.ToString());

        var loserHero = userHeroes.Aggregate((first, second) => first.Health <= second.Health ? first : second);
        var winnerHero = userHeroes.Aggregate((first, second) => first.Health <= second.Health ? second : first);

        
        await _gameRoomService.CloseGame(gameRoom);

        await SaveGameStatistic(gameRoom.Id, winnerHero.UserId, loserHero.UserId, winnerHero.Health);
        
        await _hubMassager.SendGroupNotify(
            gameRoom.Id.ToString(), 
            $"WINNER OF THE GAME {winnerHero.Username}, with {winnerHero.Health} healths"
        );
    }

    private async Task<List<UserHero>> StartGame(IEnumerable<User> usersFromRoom, string groupId)
    {
        var userHeroes = usersFromRoom.Select(x => new UserHero(x.Id, x.Username)).ToList();
        var firstUserHero = userHeroes.First();
        var secondUserHero = userHeroes.Last();

        var endGame = false;
        var round = 1;
        
        async void DealDamage(UserHero x) => 
            await _damageDealerService.DealDamage(x, new Tuple<int, int>(0, 2), groupId);

        while (true)
        {
            await _hubMassager
                .SendGroupNotify(groupId, $"ROUND {round}");
            
            userHeroes
                .AsParallel()
                .ForAll(DealDamage);
            
            if (userHeroes.Any(x => x.Health <= 0))
            {
                endGame = firstUserHero.Health != secondUserHero.Health;
            }
            
            if (endGame)
            {
                break;
            }

            round++;
            
            await Task.Delay(TimeSpan.FromSeconds(1));
        }

        return userHeroes;
    }

    private async Task SaveGameStatistic(Guid gameRoomId, Guid winnerId, Guid loserId, int winnerHealth)
    {
        var gameStatisticDto = new GameStatisticDto
        {
            GameRoomId = gameRoomId,
            WinnerId = winnerId,
            LoserId = loserId,
            WinnerHealth = winnerHealth,
        };

       await _gameStatisticService.Add(gameStatisticDto);
    }
}