namespace FaceItStats.Api.Controllers
{
    using FaceItStats.Api.Components.Queries;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class SuffixController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SuffixController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetSuffix(int counter, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetSuffixRequest(counter), cancellationToken));
        }
    }
}
