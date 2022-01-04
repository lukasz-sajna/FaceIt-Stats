using FaceItStats.Api.Client;
using FaceItStats.Api.Configs;
using FaceItStats.Api.Hubs;
using FaceItStats.Api.Persistence;
using FaceItStats.Api.Persistence.Models;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FaceItStats.Api.Components.Commands
{
    public class MatchCreatedRequestHandler : IRequestHandler<MatchCreatedRequest>
    {
        private readonly SeClient _seClient;
        private readonly FaceitDbContext _faceItDbContext;

        public MatchCreatedRequestHandler(FaceitDbContext faceItDbContext, IOptions<Auth> authSettings, IHubContext<NotificationsHub> hubContext)
        {
            _faceItDbContext = faceItDbContext;
            _seClient = new SeClient(authSettings.Value.SeToken, hubContext);
        }

        public async Task<Unit> Handle(MatchCreatedRequest request, CancellationToken cancellationToken)
        {
            var betSettings = await _faceItDbContext.BetsSettings.FirstOrDefaultAsync(cancellationToken);
            var isBetsEnabled = betSettings != null && betSettings.IsEnabled;

            var matchResult = new MatchResult(request.MatchId);

            _faceItDbContext.Add(matchResult);

            if (isBetsEnabled)
            {
                var contest = await _seClient.CreateBet();
                await _seClient.StartBet(contest.Id);

                matchResult.SetContest(contest.Id, contest.Options.First().Id, contest.Options.Last().Id);
            }

            await _faceItDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
