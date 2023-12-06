using FaceItStats.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FaceItStats.Api.Client.Models
{
    public static class FaceItResponseExtensions
    {
        public static FaceItStatsResponse ToFaceItStatsResponse(this FaceItResponse response, string playerId, PlayerMatchHistory latestMatches, List<PlayerMatchEloHistory> eloHistory, List<string> excludedCompetitions)
        {
            return new FaceItStatsResponse
            {
                PlayerId = playerId,
                Level = (int)response.Lvl,
                Elo = (int)response.Elo,
                IsEloCalculating = response.TodayEloDiff.Contains("NaN"),
                EloDiff = ConvertEloDiff(response.TodayEloDiff),
                LastResults = ConvertLastResults(latestMatches, eloHistory, playerId, excludedCompetitions),
                CurrentMatchElo = new CurrentMatchElo(response.CurrentMatch.Gain, response.CurrentMatch.Lose)
            };
        }        

        private static int ConvertEloDiff(string eloDiff)
        {
            if (eloDiff == null || eloDiff.Contains("NaN"))
            {
                return 0;
            }

            return int.Parse(eloDiff);
        }

        private static List<LastResult> ConvertLastResults(PlayerMatchHistory latestMatches, IEnumerable<PlayerMatchEloHistory> eloHistory, string playerId, ICollection<string> excludedCompetitions)
        {
            var lastResults = new List<LastResult>();

            var matchIdsWithoutElo = latestMatches.Items.Where(x => excludedCompetitions.Contains(x.CompetitionName)).Select(p => p.MatchId).ToList();
            var eloHistoryArray = eloHistory.Where(x => !matchIdsWithoutElo.Contains(x.MatchId)).ToArray();

            foreach (var result in latestMatches.Items.Where(x => !matchIdsWithoutElo.Contains(x.MatchId)))
            {
                var myFaction = result.Teams.Faction1.Players.Any(x => x.PlayerId.ToString().Equals(playerId)) ? "faction1" : "faction2";
                var isWin = result.Results.Winner.Equals(myFaction);

                var matchIndex = eloHistoryArray
                    .Select((element, index) => new { element, index })
                    .FirstOrDefault(x => x.element.MatchId.Equals(result.MatchId))?.index ?? -1;

                var lastResult = new LastResult(result.MatchId, isWin, true);

                if (matchIndex > -1 && eloHistoryArray.Length >= matchIndex + 2)
                {
                    var currentMatchElo = Convert.ToInt32(eloHistoryArray[matchIndex].Elo);
                    var previousMatchElo = Convert.ToInt32(eloHistoryArray[matchIndex + 1].Elo);

                    if (currentMatchElo > 0)
                    {
                        var eloDiff = currentMatchElo - previousMatchElo;

                        lastResult.SetElo(eloDiff);
                    }
                }

                lastResults.Add(lastResult);
            }

            return lastResults.Take(5).ToList();
        }
    }
}
