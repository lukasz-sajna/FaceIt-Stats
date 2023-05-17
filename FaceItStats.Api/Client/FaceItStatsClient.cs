namespace FaceItStats.Api.Client
{
    using FaceItStats.Api.Client.Models;
    using FaceItStats.Api.Configs;
    using FaceItStats.Api.Models;
    using Newtonsoft.Json;
    using RestSharp;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class FaceItStatsClient
    {
        private readonly ThirdPartyApis _thirdPartyApis;

        public FaceItStatsClient(ThirdPartyApis thirdPartyApis)
        {
            _thirdPartyApis = thirdPartyApis;
        }

        public async Task<FaceItResponse> GetStatsForNickname(string nickname, CancellationToken cancellationToken)
        {
            var client = new RestClient(_thirdPartyApis.satontApi.Url);

            return await client.GetAsync<FaceItResponse>(new RestRequest($"faceit?nick={nickname}&game=csgo&timezone=Europe%2FWarsaw", (Method)DataFormat.Json), cancellationToken);
        }

        public async Task<FaceItResponse> GetLadderInfoForNickname(string nickname)
        {
            var client = new RestClient("http://api.faceit.myhosting.info:81/");

            return await client.GetAsync<FaceItResponse>(new RestRequest($"?n={nickname}", (Method)DataFormat.Json));
        }

        public async Task<FaceItUserResponse> GetUserInfoForNickname(string nickname, CancellationToken cancellationToken)
        {
            var client = new RestClient($"{_thirdPartyApis.faceItApi.Url}/v4/players")
            .AddDefaultHeader("Authorization", $"Bearer {_thirdPartyApis.faceItApi.Token}");

            var response = await client.ExecuteAsync(new RestRequest($"?nickname={nickname}&game=csgo", (Method)DataFormat.Json), Method.Get, cancellationToken);

            return JsonConvert.DeserializeObject<FaceItUserResponse>(response.Content);
        }

        public async Task<MatchDetails> GetMatchDetails(string matchId, CancellationToken cancellationToken)
        {
            var client = new RestClient($"{_thirdPartyApis.faceItApi.Url}/v4/matches")
            .AddDefaultHeader("Authorization", $"Bearer {_thirdPartyApis.faceItApi.Token}");

            return await client.GetAsync<MatchDetails>(new RestRequest($"/{matchId}", (Method)DataFormat.Json), cancellationToken);
        }


        public async Task<MatchStatistics> GetStatisticOfMatch(string matchId, CancellationToken cancellationToken)
        {
            var client = new RestClient($"{_thirdPartyApis.faceItApi.Url}/v4/matches")
            .AddDefaultHeader("Authorization", $"Bearer {_thirdPartyApis.faceItApi.Token}");

            var response = await client.ExecuteAsync(new RestRequest($"/{matchId}/stats", (Method)DataFormat.Json), Method.Get, cancellationToken);

            return JsonConvert.DeserializeObject<MatchStatistics>(response.Content);
        }

        public async Task<PlayerMatchHistory> GetPlayerMatchHistory(string playerId, int limit, CancellationToken cancellationToken)
        {
            var client = new RestClient($"{_thirdPartyApis.faceItApi.Url}/v4/players")
            .AddDefaultHeader("Authorization", $"Bearer {_thirdPartyApis.faceItApi.Token}");

            var response = await client.ExecuteAsync(new RestRequest($"/{playerId}/history?game=csgo&offset=0&limit={limit}", (Method)DataFormat.Json), Method.Get, cancellationToken);

            return JsonConvert.DeserializeObject<PlayerMatchHistory>(response.Content);
        }

        public async Task<List<PlayerMatchEloHistory>> GetPlayerMatchEloHistory(string playerId, int limit, CancellationToken cancellationToken)
        {
            var client = new RestClient("https://api.faceit.com/stats/v1/stats/time/users");
            var request = new RestRequest($"/{playerId}/games/csgo?page=0&size={limit}", Method.Get);
            request.AddOrUpdateHeader("Accept", "*/*");
            request.AddOrUpdateHeader("Host", "api.faceit.com");

            var response = await client.ExecuteAsync(request, cancellationToken);

            return JsonConvert.DeserializeObject<List<PlayerMatchEloHistory>>(response.Content);
        }
    }
}
