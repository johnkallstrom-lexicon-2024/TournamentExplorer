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
            CreateMap<Tournament, TournamentWithoutRelationsDto>();
            CreateMap<TournamentCreateDto, Tournament>();
            CreateMap<TournamentUpdateDto, Tournament>().ReverseMap();

            CreateMap<Game, GameDto>();
            CreateMap<Game, GameWithoutRelationsDto>();
            CreateMap<GameCreateDto, Game>();
            CreateMap<GameUpdateDto, Game>().ReverseMap();
        }
    }
}
