using System.Collections.Generic;

namespace FaceItStats.Api.Models
{
    public class FaceItStatsResponse
    {
        public string PlayerId { get; set; }
        public int Level { get; set; }
        public int Elo { get; set; }
        public int EloDiff { get; set; }
        public bool IsEloCalculating { get; set; }
        public List<LastResult> LastResults { get; set; }
        public CurrentMatchElo CurrentMatchElo { get; set; }
    }

    public class CurrentMatchElo
    {
        public int Gain { get; private set; }
        public int Loss { get; private set; }

        public CurrentMatchElo(int gain, int loss)
        {
            Gain = gain;
            Loss = loss;
        }
    }

    public class LastResult
    {
        public string MatchId { get; private set; }
        public bool Win { get; private set; }
        public int Elo { get; private set; }
        public bool IsEloCalculating { get; set; }

        public LastResult(string result)
        {
            var splittedResult = result.Split(" ");
            if(splittedResult.Length == 2)
            {
                Win = splittedResult[0].Equals("W");
                Elo = splittedResult[1].Contains("NaN") ? 0 : int.Parse(splittedResult[1]);
                IsEloCalculating = splittedResult[1].Contains("NaN");
            }
        }

        public LastResult(string matchId, bool isWin, bool isEloCalculating)
        {
            MatchId = matchId;
            Win = isWin;
            IsEloCalculating = isEloCalculating;
        }

        public void SetElo(int elo)
        {
            Elo = elo;
            IsEloCalculating = false;
        }
    }
}
