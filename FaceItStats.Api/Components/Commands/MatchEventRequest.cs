using MediatR;

namespace FaceItStats.Api.Components.Commands
{
    public class MatchEventRequest(string matchId) : IRequest
    {
        public string MatchId { get; private set; } = matchId;
    }
}
