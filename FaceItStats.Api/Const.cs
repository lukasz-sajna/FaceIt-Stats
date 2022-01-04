namespace FaceItStats.Api
{
    internal static class Const
    {
        public static readonly string DefaultCorsPolicy = "DefaultCorsPolicy";
        public static readonly string SignalRHubsPathRoot = "/hubs";
        public static readonly string ConnectionString = @"DataSource=FaceItDb.db;";
        public static readonly string HangfireConnectionString = @"FaceItDb.db";
    }
}
