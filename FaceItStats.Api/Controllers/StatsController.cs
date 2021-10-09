using FaceItStats.Api.Client;
using FaceItStats.Api.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FaceItStats.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly FaceItStatsClient _faceItClient;

        public StatsController()
        {
            _faceItClient = new FaceItStatsClient();
        }

        [HttpGet("GetStats")]
        public async Task<IActionResult> GetFaceItStats([FromQuery]string nickname, [FromQuery]string template = null)
        {
            var stats = await _faceItClient.GetStatsForNickname(nickname);

            if(template != null)
            {
                return Ok(template.FillTemplate(stats));
            }

            return Ok(stats);
        }
    }
}
