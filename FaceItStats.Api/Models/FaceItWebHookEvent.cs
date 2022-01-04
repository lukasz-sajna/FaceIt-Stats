namespace FaceItStats.Api.Models
{
    public class FaceItWebHookEvent
    {
        public const string MatchObjectCreated = "match_object_created";
        public const string MatchStatusAborted = "match_status_aborted";
        public const string MatchStatusCancelled = "match_status_cancelled";
        public const string MatchStatusConfiguring = "match_status_configuring";
        public const string MatchStatusFinished = "match_status_finished";
        public const string MatchStatusReady = "match_status_ready";
    }
}
