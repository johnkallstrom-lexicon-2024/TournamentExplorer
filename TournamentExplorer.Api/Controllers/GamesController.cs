using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TournamentExplorer.Api.Models;
using TournamentExplorer.Core.Contracts;
using TournamentExplorer.Core.Entities;

namespace TournamentExplorer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GamesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<GameDto>> GetGames([FromQuery] string filter = "")
        {
            var games = Enumerable.Empty<Game>();

            if (!string.IsNullOrEmpty(filter))
            {
                games = _unitOfWork.GameRepository.Get(
                    navigationProperty: g => g.Tournament, 
                    filter: g => g.Name.Contains(filter));
            }
            else
            {
                games = _unitOfWork.GameRepository.Get(g => g.Tournament);
            }

            var dtos = _mapper.Map<IEnumerable<GameDto>>(games);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameDto>> GetGame(int id)
        {
            var game = await _unitOfWork.GameRepository.GetByIdAsync(id);
            if (game is null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<GameDto>(game);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateGame([FromBody] GameCreateDto dto)
        {
            var entity = _mapper.Map<Game>(dto);

            var createdGame = _unitOfWork.GameRepository.Add(entity);
            await _unitOfWork.SaveAsync();

            return CreatedAtAction(nameof(GetGame), new { createdGame.Id }, createdGame);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGame(int id, [FromBody] GameUpdateDto dto)
        {
            var game = await _unitOfWork.GameRepository.GetByIdAsync(id);
            if (game is null)
            {
                return NotFound();
            }

            game = _mapper.Map(source: dto, destination: game);
            _unitOfWork.GameRepository.Update(game);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchGame(int id, JsonPatchDocument<GameUpdateDto> patchDocument)
        {
            var game = await _unitOfWork.GameRepository.GetByIdAsync(id);
            if (game is null)
            {
                return NotFound();
            }

            // Apply patch
            var dtoToPatch = new GameUpdateDto();
            dtoToPatch = _mapper.Map(destination: dtoToPatch, source: game);

            patchDocument.ApplyTo(dtoToPatch, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validate patched dto
            if (!TryValidateModel(dtoToPatch))
            {
                return BadRequest(ModelState);
            }

            // Update entity and save to db
            game = _mapper.Map(source: dtoToPatch, destination: game);
            _unitOfWork.GameRepository.Update(game);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGame(int id)
        {
            var game = await _unitOfWork.GameRepository.GetByIdAsync(id);
            if (game is null)
            {
                return NotFound();
            }

            _unitOfWork.GameRepository.Delete(game);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}
