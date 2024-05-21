using Microsoft.AspNetCore.Mvc;
using TournamentExplorer.Core.Contracts;
using TournamentExplorer.Api.Models;
using AutoMapper;
using TournamentExplorer.Core.Entities;

namespace TournamentExplorer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TournamentsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllTournaments()
        {
            var tournaments = await _unitOfWork.TournamentRepository.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<TournamentWithoutRelationsDto>>(tournaments);

            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetTournament(int id, [FromQuery] bool includeGames)
        {
            var tournament = await _unitOfWork.TournamentRepository.GetAsync(id, includeGames);
            if (tournament is null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<TournamentDto>(tournament);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTournament([FromBody] TournamentForCreationDto dto)
        {
            var tournament = _mapper.Map<Tournament>(dto);

            var createdTournament = await _unitOfWork.TournamentRepository.CreateAsync(tournament);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(
                nameof(GetTournament), 
                new { createdTournament.Id }, 
                createdTournament);
        }
    }
}
