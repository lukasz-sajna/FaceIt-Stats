namespace FaceItStats.Api.Models
{
    public class ChallengeData(int rank, int wins, int draws, int loses)
    {
        public int Rank { get; set; } = rank;
        public int Wins { get; set; } = wins;
        public int Draws { get; set; } = draws;
        public int Loses { get; set; } = loses;
    }
}
