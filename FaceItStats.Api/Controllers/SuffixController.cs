namespace FaceItStats.Api.Controllers
{
    using Components.Queries;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class SuffixController(ISender mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetSuffix(int counter, CancellationToken cancellationToken)
        {
            return Ok(await mediator.Send(new GetSuffixRequest(counter), cancellationToken));
        }
    }
}
