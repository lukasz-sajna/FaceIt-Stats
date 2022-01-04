namespace FaceItStats.Api.Components.Commands
{
    public class MatchCancelledRequest : MatchEventRequest
    {
        public MatchCancelledRequest(string matchId) : base(matchId)
        {
        }
    }
}
