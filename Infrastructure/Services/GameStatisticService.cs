using Application.Contracts;
using Application.Dto;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class GameStatisticService : IGameStatisticService
{
    private readonly IMapper _mapper;
    private readonly IRepository<GameStatistic> _gameStatisticRepository;

    public GameStatisticService(IMapper mapper, IRepository<GameStatistic> gameStatisticRepository)
    {
        _mapper = mapper;
        _gameStatisticRepository = gameStatisticRepository;
    }

    public async Task Add(GameStatisticDto gameStatisticDto)
    {
        var entity = _mapper.Map<GameStatistic>(gameStatisticDto);
        await _gameStatisticRepository.CreateAsync(entity);
        await _gameStatisticRepository.SaveChangesAsync();
    }

    public async Task<List<GameStatisticDto>> GetListByUser(Guid userId, CancellationToken cancellationToken)
    {
        var gameStatistics = await _gameStatisticRepository
            .FindByCondition(x => x.WinnerId == userId || x.LoserId == userId, false)
            .Include(x => x.Loser)
            .Include(x => x.Winner)
            .Include(x => x.GameRoom)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<GameStatisticDto>>(gameStatistics);
    }

    public async Task<List<GameStatisticDto>> GetList(CancellationToken cancellationToken)
    {
        var gameStatistics = await _gameStatisticRepository
            .FindAll(false)
            .Include(x => x.Loser)
            .Include(x => x.Winner)
            .Include(x => x.GameRoom)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<GameStatisticDto>>(gameStatistics);
    }
}