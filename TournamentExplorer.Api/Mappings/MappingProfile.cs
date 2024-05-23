using AutoMapper;
using TournamentExplorer.Api.Models;
using TournamentExplorer.Core.Entities;

namespace TournamentExplorer.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tournament, TournamentDto>();
            CreateMap<TournamentCreateDto, Tournament>();
            CreateMap<TournamentUpdateDto, Tournament>();

            CreateMap<Game, GameDto>()
                .ForMember(dto => dto.Tournament, cfg => cfg.MapFrom(game => game.Tournament.Title));

            CreateMap<GameCreateDto, Game>();
            CreateMap<GameUpdateDto, Game>();
        }
    }
}
