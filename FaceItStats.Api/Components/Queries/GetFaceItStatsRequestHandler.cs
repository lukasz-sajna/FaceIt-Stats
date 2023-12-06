namespace FaceItStats.Api.Components.Queries
{
    using Client;
    using FaceItStats.Api.Client.Models;
    using Configs;
    using Models;
    using MediatR;
    using Microsoft.Extensions.Options;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetFaceItStatsRequestHandler(
        IOptions<ExcludedCompetitions> excludedCompetitionsOptions,
        IOptions<ThirdPartyApis> thirdPartyApisOptions)
        : IRequestHandler<GetFaceItStatsRequest, FaceItStatsResponse>
    {
        private readonly FaceItStatsClient _faceItClient = new(thirdPartyApisOptions.Value);
        private readonly ExcludedCompetitions _excludedCompetitions = excludedCompetitionsOptions.Value;

        public async Task<FaceItStatsResponse> Handle(GetFaceItStatsRequest request, CancellationToken cancellationToken)
        {
            var stats = await _faceItClient.GetStatsForNickname(request.Nickname, cancellationToken);
            var userInfo = await _faceItClient.GetUserInfoForNickname(request.Nickname, cancellationToken);
            var latestMatches = await _faceItClient.GetPlayerMatchHistory(userInfo.PlayerId, 10, cancellationToken);
            var eloHistory = await _faceItClient.GetPlayerMatchEloHistory(userInfo.PlayerId, 15, cancellationToken);

            return stats.ToFaceItStatsResponse(userInfo.PlayerId, latestMatches, eloHistory, _excludedCompetitions.Names);
        }
    }
}
