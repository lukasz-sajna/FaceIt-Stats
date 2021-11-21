using FaceItStats.Api.Models;
using System.Collections.Generic;
using System.Linq;

namespace FaceItStats.Api.Client.Models
{
    public static class FaceItResponseExtensions
    {
        public static FaceItStatsResponse ToFaceItStatsResponse(this FaceItResponse response, string playerId)
        {
            return new FaceItStatsResponse
            {
                PlayerId = playerId,
                Level = (int)response.Lvl,
                Elo = (int)response.Elo,
                IsEloCalculating = response.TodayEloDiff.Contains("NaN"),
                EloDiff = ConvertEloDiff(response.TodayEloDiff),
                LastResults = ConvertLastResults(response.LatestMatchesTrend.Extended)
            };
        }

        private static int ConvertEloDiff(string eloDiff)
        {
            if(eloDiff == null || eloDiff.Contains("NaN"))
            {
                return 0;
            }

            return int.Parse(eloDiff);
        }

        private static List<LastResult> ConvertLastResults(string lastResults)
        {
            if(lastResults == null)
            {
                return new List<LastResult>();
            }

            var results = lastResults.Split("|");

            for (int i = 0; i < results.Length; i++)
            {
                results[i] = results[i].Trim();
            }

            return results.ToList().Select(result => new LastResult(result)).ToList();
        }
    }
}
