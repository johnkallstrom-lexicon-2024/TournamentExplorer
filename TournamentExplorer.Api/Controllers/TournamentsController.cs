using Microsoft.AspNetCore.Mvc;
using TournamentExplorer.Core.Contracts;
using TournamentExplorer.Api.Models;
using AutoMapper;
using TournamentExplorer.Core.Entities;
using Microsoft.AspNetCore.JsonPatch;

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
        public ActionResult<IEnumerable<Tournament>> GetTournaments([FromQuery] bool includeGames)
        {
            IEnumerable<Tournament> tournaments = default!;
            if (includeGames) tournaments = _unitOfWork.TournamentRepository.GetListIncluding(t => t.Games);
            else tournaments = _unitOfWork.TournamentRepository.GetList();

            var dtos = _mapper.Map<IEnumerable<TournamentDto>>(tournaments);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tournament>> GetTournament(int id)
        {
            var tournament = await _unitOfWork.TournamentRepository.GetByIdAsync(id);
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

            var createdTournament = _unitOfWork.TournamentRepository.Add(tournament);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(
                nameof(GetTournament), 
                new { createdTournament.Id }, 
                createdTournament);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTournament(int id, [FromBody] TournamentUpdateDto dto)
        {
            var tournament = await _unitOfWork.TournamentRepository.GetByIdAsync(id);
            if (tournament is null)
            {
                return NotFound();
            }

            tournament = _mapper.Map(source: dto, destination: tournament);

            _unitOfWork.TournamentRepository.Update(tournament);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchTournament(int id, JsonPatchDocument<TournamentUpdateDto> patchDocument)
        {
            var tournament = await _unitOfWork.TournamentRepository.GetByIdAsync(id);
            if (tournament is null)
            {
                return NotFound();
            }

            var dtoToPatch = new TournamentUpdateDto();
            dtoToPatch = _mapper.Map(source: tournament, destination: dtoToPatch);

            patchDocument.ApplyTo(dtoToPatch, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(dtoToPatch))
            {
                return BadRequest(ModelState);
            }

            tournament = _mapper.Map(source: dtoToPatch, destination: tournament);
            _unitOfWork.TournamentRepository.Update(tournament);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTournament(int id)
        {
            var tournament = await _unitOfWork.TournamentRepository.GetByIdAsync(id);
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
