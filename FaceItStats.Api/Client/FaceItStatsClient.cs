namespace FaceItStats.Api.Client
{
    using Models;
    using Configs;
    using FaceItStats.Api.Models;
    using Newtonsoft.Json;
    using RestSharp;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class FaceItStatsClient(ThirdPartyApis thirdPartyApis)
    {
        public Task<FaceItResponse> GetStatsForNickname(string nickname, CancellationToken cancellationToken)
        {
            var client = new RestClient(thirdPartyApis.SatontApi.Url);

            return client.GetAsync<FaceItResponse>(new RestRequest($"faceit?nick={nickname}&game=cs2&timezone=Europe%2FWarsaw"), cancellationToken);
        }

        public Task<FaceItResponse> GetLadderInfoForNickname(string nickname)
        {
            var client = new RestClient("http://api.faceit.myhosting.info:81/");

            return client.GetAsync<FaceItResponse>(new RestRequest($"?n={nickname}"));
        }

        public async Task<FaceItUserResponse> GetUserInfoForNickname(string nickname, CancellationToken cancellationToken)
        {
            var client = new RestClient($"{thirdPartyApis.FaceItApi.Url}/v4/players")
            .AddDefaultHeader("Authorization", $"Bearer {thirdPartyApis.FaceItApi.Token}");

            var response = await client.ExecuteAsync(new RestRequest($"?nickname={nickname}&game=cs2"), Method.Get, cancellationToken);

            return response.Content is not null ? JsonConvert.DeserializeObject<FaceItUserResponse>(response.Content) : new FaceItUserResponse();
        }

        public Task<MatchDetails> GetMatchDetails(string matchId, CancellationToken cancellationToken)
        {
            var client = new RestClient($"{thirdPartyApis.FaceItApi.Url}/v4/matches")
            .AddDefaultHeader("Authorization", $"Bearer {thirdPartyApis.FaceItApi.Token}");

            return client.GetAsync<MatchDetails>(new RestRequest($"/{matchId}"), cancellationToken);
        }


        public async Task<MatchStatistics> GetStatisticOfMatch(string matchId, CancellationToken cancellationToken)
        {
            var client = new RestClient($"{thirdPartyApis.FaceItApi.Url}/v4/matches")
            .AddDefaultHeader("Authorization", $"Bearer {thirdPartyApis.FaceItApi.Token}");

            var response = await client.ExecuteAsync(new RestRequest($"/{matchId}/stats"), Method.Get, cancellationToken);

            return response.Content is not null ? JsonConvert.DeserializeObject<MatchStatistics>(response.Content) : new MatchStatistics();
        }

        public async Task<PlayerMatchHistory> GetPlayerMatchHistory(string playerId, int limit, CancellationToken cancellationToken)
        {
            var client = new RestClient($"{thirdPartyApis.FaceItApi.Url}/v4/players")
            .AddDefaultHeader("Authorization", $"Bearer {thirdPartyApis.FaceItApi.Token}");

            var response = await client.ExecuteAsync(new RestRequest($"/{playerId}/history?game=cs2&offset=0&limit={limit}"), Method.Get, cancellationToken);

            return response.Content is not null ? JsonConvert.DeserializeObject<PlayerMatchHistory>(response.Content) : new PlayerMatchHistory();
        }

        public async Task<List<PlayerMatchEloHistory>> GetPlayerMatchEloHistory(string playerId, int limit, CancellationToken cancellationToken)
        {
            var client = new RestClient("https://api.faceit.com/stats/v1/stats/time/users");
            var request = new RestRequest($"/{playerId}/games/cs2?page=0&size={limit}");
            request.AddOrUpdateHeader("Accept", "*/*");
            request.AddOrUpdateHeader("Host", "api.faceit.com");

            var response = await client.ExecuteAsync(request, cancellationToken);

            return response.Content is not null ? JsonConvert.DeserializeObject<List<PlayerMatchEloHistory>>(response.Content) : new List<PlayerMatchEloHistory>();
        }
    }
}
