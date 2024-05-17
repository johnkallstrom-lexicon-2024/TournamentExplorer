using Microsoft.AspNetCore.Mvc;

namespace TournamentExplorer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        public ActionResult Ping() => Ok("Hello World!");
    }
}
