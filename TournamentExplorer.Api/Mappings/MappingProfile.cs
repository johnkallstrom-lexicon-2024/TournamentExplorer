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
            CreateMap<TournamentForCreationDto, Tournament>();

            CreateMap<Game, GameDto>();
            CreateMap<Game, GameWithoutRelationsDto>();
        }
    }
}
