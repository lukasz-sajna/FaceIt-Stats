using FaceItStats.Api.Client.Models;
using FaceItStats.Api.Hubs;
using FaceItStats.Api.Models;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace FaceItStats.Api.Client
{
    public class SeClient
    {
        private readonly RestClient _seClient;

        private readonly List<ContestRequest> _contests = new();
        private readonly IHubContext<NotificationsHub> _hubContext;

        private SeClient(string token, IHubContext<NotificationsHub> hubContext)
        {
            _seClient = new RestClient(@"https://api.streamelements.com/kappa/v2/contests/59f8bf5de889a60001e576f3");
            _seClient.AddDefaultHeader("Authorization", $"Bearer {token}");
            CreateContests();
            _hubContext = hubContext;
        }

        public static SeClient CreateInstance(string token, IHubContext<NotificationsHub> hubContext)
        {
            return new SeClient(token, hubContext);
        }

        public Task<ContestResponse> CreateBet()
        {
            var req = new RestRequest
            {
                Method = Method.Post,
                RequestFormat = DataFormat.Json
            };

            req.AddBody(JsonConvert.SerializeObject(GetRandomContest()));

            return ExecuteRequestAsync<ContestResponse>(req);
        }

        public Task<ContestResponse> StartBet(string contestId)
        {
            var req = new RestRequest
            {
                Method = Method.Put,
                RequestFormat = DataFormat.Json,
                Resource = $"/{contestId}/start"
            };

            return ExecuteRequestAsync<ContestResponse>(req);
        }

        public Task<ContestResponse> GetBet(string contestId)
        {
            var req = new RestRequest
            {
                Method = Method.Get,
                RequestFormat = DataFormat.Json,
                Resource = $"/{contestId}"
            };

            return ExecuteRequestAsync<ContestResponse>(req);
        }

        public Task<string> CancelBet(string contestId)
        {
            var req = new RestRequest
            {
                Method = Method.Delete,
                RequestFormat = DataFormat.Json,
                Resource = $"/{contestId}/close"
            };

            req.AddBody(JsonConvert.SerializeObject(new { id = contestId }));

            return ExecuteRequestAsync<string>(req);
        }

        public Task<string> RefundBet(string contestId)
        {
            var req = new RestRequest
            {
                Method = Method.Delete,
                RequestFormat = DataFormat.None,
                Resource = $"/{contestId}/refund"
            };

            return ExecuteRequestAsync<string>(req);
        } 

        public Task<string> PickWinner(string contestId, string winnerId)
        {
            var req = new RestRequest
            {
                Method = Method.Put,
                RequestFormat = DataFormat.Json,
                Resource = $"/{contestId}/winner"
            };

            req.AddBody(JsonConvert.SerializeObject(new Winner(winnerId)));

            return ExecuteRequestAsync<string>(req);
        }

        private async Task<T> ExecuteRequestAsync<T>(RestRequest request)
        {
            var response = await _seClient.ExecuteAsync<T>(request);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await _hubContext.Clients.All.SendAsync("notification", new NotificationData("Stream Elements token expired", NotificationType.Error));
                throw new Exception();
            }

            return JsonConvert.DeserializeObject<T>(response.Content!);
        }

        private ContestRequest GetRandomContest()
        {
            var index = new Random().Next(_contests.Count);

            return _contests[index];
        }

        private void CreateContests()
        {
            _contests.Add(new ContestRequest
            {
                Title = ContestType.WinLose,
                MinBet = 10,
                MaxBet = 10000,
                Duration = 1,
                BotResponses = true,
                Options =
                [
                    new OptionRequest { Title = "Win", Command = "win" },
                    new OptionRequest { Title = "Lose", Command = "lose" }
                ]
            });

            _contests.Add(new ContestRequest
            {
                Title = ContestType.Kd,
                MinBet = 10,
                MaxBet = 10000,
                Duration = 1,
                BotResponses = true,
                Options =
                [
                    new() { Title = "Over 0.995", Command = "over" },
                    new () { Title = "Under 0.995", Command = "under" }
                ]
            });

            _contests.Add(new ContestRequest
            {
                Title = ContestType.Kills,
                MinBet = 10,
                MaxBet = 10000,
                Duration = 1,
                BotResponses = true,
                Options =
                [
                    new() { Title = "Over 19.5", Command = "over" },
                    new() { Title = "Under 19.5", Command = "under" }
                ]
            });
        }
    }
}
