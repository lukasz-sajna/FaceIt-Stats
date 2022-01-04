namespace FaceItStats.Api.Components.Commands
{
    public class MatchReadyRequest : MatchEventRequest
    {
        public MatchReadyRequest(string matchId) : base(matchId)
        {
        }
    }
}
