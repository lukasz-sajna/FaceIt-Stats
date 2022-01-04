namespace FaceItStats.Api.Components.Commands
{
    public class MatchFinishedRequest : MatchEventRequest
    {

        public MatchFinishedRequest(string matchId) : base(matchId)
        {
        }
    }
}
