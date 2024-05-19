using Microsoft.AspNetCore.Mvc;
using TournamentExplorer.Core.Contracts;
using TournamentExplorer.Api.Models;
using AutoMapper;

namespace TournamentExplorer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITournamentRepository _tournamentRepository;

        public TournamentsController(ITournamentRepository tournamentRepository, IMapper mapper)
        {
            _tournamentRepository = tournamentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllTournaments()
        {
            var tournaments = await _tournamentRepository.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<TournamentWithoutRelationsDto>>(tournaments);

            return Ok(dtos);
        }
    }
}
