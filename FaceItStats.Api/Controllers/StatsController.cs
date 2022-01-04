using FaceItStats.Api.Components.Commands;
using FaceItStats.Api.Components.Queries;
using FaceItStats.Api.Helpers;
using FaceItStats.Api.Hubs;
using FaceItStats.Api.Models;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace FaceItStats.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly IHubContext<FaceItStatsHub> _hubContext;
        private readonly IMediator _mediator;

        public StatsController(IHubContext<FaceItStatsHub> hubContext, IMediator mediator)
        {
            _hubContext = hubContext;
            _mediator = mediator;
        }

        [HttpGet("GetStats")]
        public async Task<IActionResult> GetFaceItStats([FromQuery]string nickname, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetFaceItStatsRequest(nickname)));
        }

        [HttpPost("FaceItWebhook")]
        public async Task<IActionResult> FaceItWebhook([FromBody]FaceitWebhookModel body, CancellationToken cancellationToken)
        {
            if (body.Event.Equals(FaceItWebHookEvent.MatchStatusConfiguring))
            {
                BackgroundJob.Enqueue(() => HangFireHelpers.MatchEvent(new MatchCreatedRequest(body.Payload.Id)));
            }

            if (body.Event.Equals(FaceItWebHookEvent.MatchStatusReady))
            {
                BackgroundJob.Enqueue(() => HangFireHelpers.MatchEvent(new MatchReadyRequest(body.Payload.Id)));
            }


            if (body.Event.Equals(FaceItWebHookEvent.MatchStatusCancelled) || body.Event.Equals(FaceItWebHookEvent.MatchStatusAborted))
            {
                BackgroundJob.Enqueue(() => HangFireHelpers.MatchEvent(new MatchCancelledRequest(body.Payload.Id)));
            }


            if (body.Event.Equals(FaceItWebHookEvent.MatchStatusFinished))
            {
                BackgroundJob.Enqueue(() => HangFireHelpers.MatchEvent(new MatchFinishedRequest(body.Payload.Id)));
            }

            await _hubContext.Clients.All.SendAsync(body.Event, body.ThirdPartyId.ToString(), cancellationToken);

            return NoContent();
        }

        [HttpPost("BetState")]
        public IActionResult ChangeBetState([FromQuery] bool isEnabled, CancellationToken cancellationToken)
        {

            BackgroundJob.Enqueue(() => HangFireHelpers.SetBetState( new SetBetStateRequest(isEnabled)));

            return NoContent();
        }

        [HttpGet("BetState")]
        public async Task<IActionResult> GetBetState(CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetBetStateRequest()));
        }
    }
}
