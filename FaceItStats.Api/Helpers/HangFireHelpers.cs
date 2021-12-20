using System.ComponentModel;
using FaceItStats.Api.Components.Commands;
using Hangfire;
using MediatR;

namespace FaceItStats.Api.Helpers 
{ 
    public static class HangFireHelpers
    {
        public static IMediator Mediator { private get; set; }

        [AutomaticRetry(Attempts = 3)]
        [DisplayName("Processing command {0}")]
        public static void ResetStats(ResetStatsCommand command)
        {
            Mediator.Send(command);
        }
    }
}
