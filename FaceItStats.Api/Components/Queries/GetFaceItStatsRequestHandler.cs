namespace FaceItStats.Api.Components.Queries
{
    using FaceItStats.Api.Client;
    using FaceItStats.Api.Client.Models;
    using FaceItStats.Api.Configs;
    using FaceItStats.Api.Models;
    using MediatR;
    using Microsoft.Extensions.Options;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetFaceItStatsRequestHandler : IRequestHandler<GetFaceItStatsRequest, FaceItStatsResponse>
    {
        private readonly FaceItStatsClient _faceItClient;
        private readonly ExcludedCompetitions _excludedCompetitions;

        public GetFaceItStatsRequestHandler(IOptions<ExcludedCompetitions> excludedCompetitionsOptions, IOptions<ThirdPartyApis> thirdPartyApisOptions)
        {
            _excludedCompetitions = excludedCompetitionsOptions.Value;
            _faceItClient = new FaceItStatsClient(thirdPartyApisOptions.Value);
        }

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
