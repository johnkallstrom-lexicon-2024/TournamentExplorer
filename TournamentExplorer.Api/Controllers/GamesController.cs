using AutoMapper;
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
        public ActionResult<IEnumerable<GameDto>> GetGames()
        {
            var games = _unitOfWork.GameRepository.GetListIncluding(g => g.Tournament);

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

            entity = null;

            var createdGame = _unitOfWork.GameRepository.Add(entity);
            await _unitOfWork.CompleteAsync();

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
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchGame(int id)
        {
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
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
