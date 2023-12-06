namespace FaceItStats.Api.Configs
{
    public class ThirdPartyApis
    {
        public SatontApi SatontApi { get; set; }

        public FaceItApi FaceItApi { get; set; }
    }

    public class SatontApi
    {
        public string Url { get; set; }
    }

    public class FaceItApi
    {
        public string Url { get; set; }

        public string Token { get; set; }
    }
}
