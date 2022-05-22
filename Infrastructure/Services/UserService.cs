using Application.Contracts;
using Application.Dto;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> _repository;
    private readonly IMapper _mapper;

    public UserService(IRepository<User> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Guid> Add(UserDto userDto, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(userDto);

        await _repository.CreateAsync(user, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return user.Id;
    }

    public async Task<List<UserDto>> GetList(CancellationToken cancellationToken)
    {
        var users = await  _repository.FindAll(false).ToListAsync(cancellationToken: cancellationToken);
        return _mapper.Map<List<UserDto>>(users);
    }
}