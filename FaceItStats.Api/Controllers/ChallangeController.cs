namespace FaceItStats.Api.Controllers
{
    using Components.Commands;
    using Components.Queries;
    using Models;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class ChallengeController(ISender mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetChallengeData(CancellationToken cancellationToken)
        {
            return Ok(await mediator.Send(new GetChallengeDataRequest(), cancellationToken));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateChallengeData([FromQuery] int rank, int wins, int draws, int loses, CancellationToken cancellationToken)
        {
            await mediator.Send(new UpdateChallengeDataRequest(new ChallengeData(rank, wins, draws, loses)), cancellationToken);

            return Ok();
        }
    }
}
