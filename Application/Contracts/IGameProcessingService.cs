using Domain.Entities;

namespace Application.Contracts;

public interface IGameProcessingService
{
    public Task InvokeGameProcess(GameRoom gameRoom);
}