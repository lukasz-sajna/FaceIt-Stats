using MediatR;

namespace FaceItStats.Api.Components.Commands
{
    public class MatchEventRequest : IRequest
    {
        public string MatchId { get; private set; }

        public MatchEventRequest(string matchId)
        {
            MatchId = matchId;
        }
    }
}
