using Application.Dto;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Services;

public class AutoMapperService : Profile
{
    public AutoMapperService()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<GameRoom, GameRoomDto>().ReverseMap();
        CreateMap<GameStatistic, GameStatisticDto>()
            .ForMember(
                dto => dto.GameRoomName,
                opt => 
                    opt.MapFrom(entity 
                        => entity.GameRoom.RoomName
                    )
            )
            .ForMember(
                dto => dto.WinnerUsername,
                opt => 
                    opt.MapFrom(entity 
                        => entity.Winner.Username
                    )
            )
            .ForMember(
                dto => dto.LoserUsername,
                opt => 
                    opt.MapFrom(entity 
                        => entity.Loser.Username
                    )
            );
        CreateMap<GameStatisticDto, GameStatistic>();
    }
}