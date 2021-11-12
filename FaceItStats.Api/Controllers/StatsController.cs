﻿using FaceItStats.Api.Client;
using FaceItStats.Api.Helpers;
using FaceItStats.Api.Hubs;
using FaceItStats.Api.Models;
using FaceItStats.Api.Persistence;
using FaceItStats.Api.Persistence.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace FaceItStats.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly FaceItStatsClient _faceItClient;
        private readonly FaceitDbContext _faceItDbContext;
        private readonly IHubContext<FaceItStatsHub> _hubContext;

        public StatsController(FaceitDbContext faceItDbContext, IHubContext<FaceItStatsHub> hubContext)
        {
            _faceItClient = new FaceItStatsClient();
            _faceItDbContext = faceItDbContext;
            _hubContext = hubContext;
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

        [HttpPost("FaceItWebhook")]
        public async Task<IActionResult> FaceItWebhook([FromBody]FaceitWebhookModel body)
        {
            var bodyString = JsonConvert.SerializeObject(body);

            _faceItDbContext.Add(new FaceitWebhookData(bodyString));
            await _faceItDbContext.SaveChangesAsync();

            await _hubContext.Clients.All.SendAsync(body.Event);

            return Ok();
        }
    }
}
