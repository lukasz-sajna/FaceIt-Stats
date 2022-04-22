namespace FaceItStats.Api.Controllers
{
    using FaceItStats.Api.Components.Commands;
    using FaceItStats.Api.Components.Queries;
    using FaceItStats.Api.Hubs;
    using FaceItStats.Api.Models;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;
    using System.Threading;
    using System.Threading.Tasks;

    public class ChallangeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChallangeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("challange")]
        public async Task<IActionResult> GetChallangeData(CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetChallangeDataRequest(), cancellationToken));
        }

        [HttpPost("challange")]
        public async Task<IActionResult> UpdateChallangeData([FromBody] ChallangeData challange, CancellationToken cancellationToken)
        {
            await _mediator.Send(new UpdateChallangeDataRequest(challange), cancellationToken);

            return Ok();
        }
    }
}
