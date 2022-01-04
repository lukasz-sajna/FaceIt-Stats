using FaceItStats.Api.Components.Commands;
using Hangfire;
using MediatR;
using System.ComponentModel;

namespace FaceItStats.Api.Helpers
{
    public static class HangFireHelpers
    {
        public static IMediator Mediator { private get; set; }

        [AutomaticRetry(Attempts = 10)]
        [DisplayName("Processing command {0}")]
        [Queue(Queues.Bets)]
        public static void SetBetState(SetBetStateRequest command)
        {
            Mediator.Send(command);
        }

        [AutomaticRetry(Attempts = 10)]
        [DisplayName("Processing command {0}")]
        [Queue(Queues.Bets)]
        public static void MatchEvent(MatchEventRequest command)
        {
            Mediator.Send(command);
        }
    }
}
