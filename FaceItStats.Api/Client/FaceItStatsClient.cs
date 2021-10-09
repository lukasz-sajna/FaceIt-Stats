using FaceItStats.Api.Client.Models;
using RestSharp;
using System.Threading.Tasks;

namespace FaceItStats.Api.Client
{
    public class FaceItStatsClient
    {
        public async Task<FaceItResponse> GetStatsForNickname(string nickname)
        {
            var client = new RestClient("https://api.satont.ru");

            return await client.GetAsync<FaceItResponse>(new RestRequest($"faceit?nick={nickname}&game=csgo", DataFormat.Json));
        }

        public async Task<FaceItResponse> GetLadderInfoForNickname(string nickname)
        {
            var client = new RestClient("http://api.faceit.myhosting.info:81/");

            return await client.GetAsync<FaceItResponse>(new RestRequest($"/?n={nickname}", DataFormat.Json));
        }
    }
}
