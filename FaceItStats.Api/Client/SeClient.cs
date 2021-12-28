using FaceItStats.Api.Client.Models;
using FaceItStats.Api.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FaceItStats.Api.Client
{
    public class SeClient
    {
        private readonly RestClient _seClient;

        private readonly List<ContenstRequest> Contests = new List<ContenstRequest>();

        public SeClient(string token)
        {
            _seClient = new RestClient(@"https://api.streamelements.com/kappa/v2/contests/59f8bf5de889a60001e576f3/");
            _seClient.AddDefaultHeader("Authorization", string.Format("Bearer {0}", token));
            CreateContests();
        }

        public async Task<ContenstResponse> CreateBet()
        {
            return await _seClient.PostAsync<ContenstResponse>(new RestRequest().AddJsonBody(GetRandomContest()));
        }

        public async Task<ContenstResponse> StartBet(string contestId)
        {
            return await _seClient.PutAsync<ContenstResponse>(new RestRequest($"{contestId}", DataFormat.Json));
        }

        public async Task<ContenstResponse> GetBet(string contestId)
        {
            return await _seClient.GetAsync<ContenstResponse>(new RestRequest($"{contestId}", DataFormat.Json));
        }

        public async Task<string> CancelBet(string contestId)
        {
            return await _seClient.DeleteAsync<string>(new RestRequest($"{contestId}/close", DataFormat.Json).AddJsonBody(new { id = contestId }));
        }

        public async Task<string> RefundBet(string contestId)
        {
            return await _seClient.DeleteAsync<string>(new RestRequest($"{contestId}/refund", DataFormat.Json));
        }

        public async Task<string> PickWinner(string contestId, string winnerId)
        {

            return await _seClient.PutAsync<string>(new RestRequest($"{contestId}/winner", DataFormat.Json).AddJsonBody(new { winnerId = winnerId }));
        }

        private ContenstRequest GetRandomContest()
        {
            var index = new Random().Next(Contests.Count);

            return Contests[index];
        }

        private void CreateContests()
        {
            Contests.Add(new ContenstRequest
            {
                Title = ContestType.WinLose,
                MinBet = 10,
                MaxBet = 10000,
                Duration = 5,
                BotResponses = true,
                Options = new List<OptionRequest> { new OptionRequest { Title = "Win", Command = "win" }, new OptionRequest { Title = "Lose", Command = "lose" } }
            });

            Contests.Add(new ContenstRequest
            {
                Title = ContestType.Kd,
                MinBet = 10,
                MaxBet = 10000,
                Duration = 5,
                BotResponses = true,
                Options = new List<OptionRequest> { new OptionRequest { Title = "Over 0.995", Command = "over" }, new OptionRequest { Title = "Under 0.995", Command = "under" } }
            });

            Contests.Add(new ContenstRequest
            {
                Title = ContestType.Kills,
                MinBet = 10,
                MaxBet = 10000,
                Duration = 5,
                BotResponses = true,
                Options = new List<OptionRequest> { new OptionRequest { Title = "Over 19.5", Command = "over" }, new OptionRequest { Title = "Under 19.5", Command = "under" } }
            });
        }
    }
}
