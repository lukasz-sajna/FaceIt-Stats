namespace FaceItStats.Api.Controllers
{
    using System.Threading;
    using System.Threading.Tasks;
    using FaceItStats.Api.Configs;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;

    [Route("api/[controller]")]
    [ApiController]
    public class DeveloperController : ControllerBase
    {
        private readonly IOptions<Test> _options;

        public DeveloperController(IOptions<Test> options)
        {
            _options = options;
        }

        [HttpGet("GetConfig")]
        public IActionResult GetConfig()
        {
            var config = _options.Value;

            return Ok(config);
        }
    }
}
