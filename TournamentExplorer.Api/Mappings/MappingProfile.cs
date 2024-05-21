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

            CreateMap<Game, GameDto>();
        }
    }
}
