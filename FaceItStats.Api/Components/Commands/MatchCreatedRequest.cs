namespace FaceItStats.Api.Components.Commands
{
    public class MatchCreatedRequest : MatchEventRequest
    {
        public MatchCreatedRequest(string matchId) : base(matchId)
        {
        }
    }
}
