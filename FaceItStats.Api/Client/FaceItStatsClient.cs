using FaceItStats.Api.Client.Models;
using FaceItStats.Api.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Threading;
using System.Threading.Tasks;

namespace FaceItStats.Api.Client
{
    public class FaceItStatsClient
    {
        public async Task<FaceItResponse> GetStatsForNickname(string nickname, CancellationToken cancellationToken)
        {
            var client = new RestClient("https://api.satont.ru");

            return await client.GetAsync<FaceItResponse>(new RestRequest($"faceit?nick={nickname}&game=csgo", DataFormat.Json), cancellationToken);
        }

        public async Task<FaceItResponse> GetLadderInfoForNickname(string nickname)
        {
            var client = new RestClient("http://api.faceit.myhosting.info:81/");

            return await client.GetAsync<FaceItResponse>(new RestRequest($"?n={nickname}", DataFormat.Json));
        }

        public async Task<FaceItUserResponse> GetUserInfoForNickname(string nickname, CancellationToken cancellationToken)
        {
            var client = new RestClient("https://open.faceit.com/data/v4/players")
            .AddDefaultHeader("Authorization", "Bearer 93ec930c-5e03-418e-b5e6-687168d87f2c");

            return await client.GetAsync<FaceItUserResponse>(new RestRequest($"?nickname={nickname}&game=csgo", DataFormat.Json));
        }

        public async Task<MatchDetails> GetMatchDetails(string matchId, CancellationToken cancellationToken)
        {
            var client = new RestClient("https://open.faceit.com/data/v4/matches")
            .AddDefaultHeader("Authorization", "Bearer 93ec930c-5e03-418e-b5e6-687168d87f2c");

            return await client.GetAsync<MatchDetails>(new RestRequest($"/{matchId}", DataFormat.Json), cancellationToken);
        }


        public async Task<MatchStatistics> GetStatisticOfMatch(string matchId, CancellationToken cancellationToken)
        {
            var client = new RestClient("https://open.faceit.com/data/v4/matches")
            .AddDefaultHeader("Authorization", "Bearer 93ec930c-5e03-418e-b5e6-687168d87f2c");

            var response = await client.ExecuteAsync(new RestRequest($"/{matchId}/stats", DataFormat.Json), Method.GET, cancellationToken);

            return JsonConvert.DeserializeObject<MatchStatistics>(response.Content);
        }
    }
}
