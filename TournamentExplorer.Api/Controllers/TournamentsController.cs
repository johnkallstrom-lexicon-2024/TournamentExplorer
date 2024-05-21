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
        public async Task<ActionResult> GetAllTournaments([FromQuery] bool includeGames)
        {
            var tournaments = await _unitOfWork.TournamentRepository.GetAllAsync(includeGames);
            var dtos = _mapper.Map<IEnumerable<TournamentDto>>(tournaments);

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
        public async Task<ActionResult> CreateTournament([FromBody] TournamentCreateDto dto)
        {
            var tournament = _mapper.Map<Tournament>(dto);

            var createdTournament = await _unitOfWork.TournamentRepository.CreateAsync(tournament);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(
                nameof(GetTournament), 
                new { createdTournament.Id }, 
                createdTournament);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTournament(int id, [FromBody] TournamentUpdateDto dto)
        {
            var tournament = await _unitOfWork.TournamentRepository.GetAsync(id);
            if (tournament is null)
            {
                return NotFound();
            }

            tournament = _mapper.Map(source: dto, destination: tournament);

            _unitOfWork.TournamentRepository.Update(tournament);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTournament(int id)
        {
            var tournament = await _unitOfWork.TournamentRepository.GetAsync(id);
            if (tournament is null)
            {
                return NotFound();
            }

            _unitOfWork.TournamentRepository.Delete(tournament);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
