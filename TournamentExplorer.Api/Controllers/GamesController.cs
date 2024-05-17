using Microsoft.AspNetCore.Mvc;

namespace TournamentExplorer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        [HttpGet]
        public ActionResult Ping() => Ok(nameof(GamesController));
    }
}
