using Application.Dto;

namespace Application.Contracts;

public interface IGameStatisticService
{
    public Task Add(GameStatisticDto gameStatisticDto);
    public Task<List<GameStatisticDto>> GetListByUser(Guid userId, CancellationToken cancellationToken);
    public Task<List<GameStatisticDto>> GetList(CancellationToken cancellationToken);
}