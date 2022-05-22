using Application.Dto;

namespace Application.Contracts;

public interface IUserService
{
    public Task<Guid> Add(UserDto userDto, CancellationToken cancellationToken);
    public Task<List<UserDto>> GetList(CancellationToken cancellationToken);
}