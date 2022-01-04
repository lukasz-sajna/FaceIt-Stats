using FaceItStats.Api.Client.Models;
using FaceItStats.Api.Hubs;
using FaceItStats.Api.Models;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace FaceItStats.Api.Client
{
    public class SeClient
    {
        private readonly RestClient _seClient;
        private readonly HttpClient _http_client;

        private readonly List<ContenstRequest> Contests = new List<ContenstRequest>();
        private readonly IHubContext<NotificationsHub> _hubContext;

        public SeClient(string token, IHubContext<NotificationsHub> hubContext)
        {
            _seClient = new RestClient(@"https://api.streamelements.com/kappa/v2/contests/59f8bf5de889a60001e576f3");
            _seClient.AddDefaultHeader("Authorization", string.Format("Bearer {0}", token));
            CreateContests();
            _hubContext = hubContext;
        }

        public async Task<ContenstResponse> CreateBet()
        {
            var req = new RestRequest
            {
                Method = Method.POST,
                RequestFormat = DataFormat.Json
            };

            req.AddBody(JsonConvert.SerializeObject(GetRandomContest()));

            return await ExecuteRequestAsync<ContenstResponse>(req);
        }

        public async Task<ContenstResponse> StartBet(string contestId)
        {
            var req = new RestRequest
            {
                Method = Method.PUT,
                RequestFormat = DataFormat.Json,
                Resource = $"/{contestId}/start"
            };

            return await ExecuteRequestAsync<ContenstResponse>(req);
        }

        public async Task<ContenstResponse> GetBet(string contestId)
        {
            var req = new RestRequest
            {
                Method = Method.GET,
                RequestFormat = DataFormat.Json,
                Resource = $"/{contestId}"
            };

            return await ExecuteRequestAsync<ContenstResponse>(req);
        }

        public async Task<string> CancelBet(string contestId)
        {
            var req = new RestRequest
            {
                Method = Method.DELETE,
                RequestFormat = DataFormat.Json,
                Resource = $"/{contestId}/close"
            };

            req.AddBody(JsonConvert.SerializeObject(new { id = contestId }));

            return await ExecuteRequestAsync<string>(req);
        }

        public async Task<string> RefundBet(string contestId)
        {
            var req = new RestRequest
            {
                Method = Method.DELETE,
                RequestFormat = DataFormat.None,
                Resource = $"/{contestId}/refund"
            };

            return await ExecuteRequestAsync<string>(req);
        } 

        public async Task<string> PickWinner(string contestId, string winnerId)
        {
            var req = new RestRequest
            {
                Method = Method.PUT,
                RequestFormat = DataFormat.Json,
                Resource = $"/{contestId}/winner"
            };

            req.AddBody(JsonConvert.SerializeObject(new Winner(winnerId)));

            return await ExecuteRequestAsync<string>(req);
        }

        private async Task<T> ExecuteRequestAsync<T>(RestRequest request)
        {
            var response = await _seClient.ExecuteAsync<T>(request);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await _hubContext.Clients.All.SendAsync("notification", new NotificationData("Stream Elements token expired", NotificationType.Error));
                throw new Exception();
            }

            return JsonConvert.DeserializeObject<T>(response.Content);
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
                Duration = 1,
                BotResponses = true,
                Options = new List<OptionRequest> { new OptionRequest { Title = "Win", Command = "win" }, new OptionRequest { Title = "Lose", Command = "lose" } }
            });

            Contests.Add(new ContenstRequest
            {
                Title = ContestType.Kd,
                MinBet = 10,
                MaxBet = 10000,
                Duration = 1,
                BotResponses = true,
                Options = new List<OptionRequest> { new OptionRequest { Title = "Over 0.995", Command = "over" }, new OptionRequest { Title = "Under 0.995", Command = "under" } }
            });

            Contests.Add(new ContenstRequest
            {
                Title = ContestType.Kills,
                MinBet = 10,
                MaxBet = 10000,
                Duration = 1,
                BotResponses = true,
                Options = new List<OptionRequest> { new OptionRequest { Title = "Over 19.5", Command = "over" }, new OptionRequest { Title = "Under 19.5", Command = "under" } }
            });
        }
    }
}
