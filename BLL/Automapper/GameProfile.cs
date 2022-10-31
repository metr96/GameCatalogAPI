using AutoMapper;
using Games.BLL.DTO;
using Games.DAL.Entities;

namespace Games.BLL.Automapper;

public class GameProfile : Profile
{
    public GameProfile()
    {
        CreateMap<Game, GameDTO>()
            .ForMember(dest =>
                dest.Genres, opt =>
                    opt.MapFrom(src =>
                        src.Genres
                            .Select(x => x.Name)
                            .ToList()));
        CreateMap<GameDTO, Game>()
            .ForMember(dest => dest.Genres, opt => opt.Ignore());

        CreateMap<CreateGameDTO, Game>()
            .ForMember(dest => dest.Genres, opt => opt.Ignore());
            
        CreateMap<Genre, GenreDTO>().ReverseMap();
    }
}