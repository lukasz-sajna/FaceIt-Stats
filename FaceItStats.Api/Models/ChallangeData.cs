namespace FaceItStats.Api.Models
{
    public class ChallangeData
    {
        public int Rank { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Loses { get; set; }

        public ChallangeData(int rank, int wins, int draws, int loses)
        {
            Rank = rank;
            Wins = wins;
            Draws = draws;
            Loses = loses;
        }
    }
}
