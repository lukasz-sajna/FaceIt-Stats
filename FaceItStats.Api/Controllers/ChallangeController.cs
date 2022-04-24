namespace FaceItStats.Api.Controllers
{
    using FaceItStats.Api.Components.Commands;
    using FaceItStats.Api.Components.Queries;
    using FaceItStats.Api.Models;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class ChallangeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChallangeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetChallangeData(CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetChallangeDataRequest(), cancellationToken));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateChallangeData([FromQuery] int rank, int wins, int draws, int loses, CancellationToken cancellationToken)
        {
            await _mediator.Send(new UpdateChallangeDataRequest(new ChallangeData(rank, wins, draws, loses)), cancellationToken);

            return Ok();
        }
    }
}
