namespace FaceItStats.Api.Persistence.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ChallengeStats
    {
        [Key]
        public int Id { get; set; }

        [Range(0, 18)]
        public int Rank { get; set; }

        public int Wins { get; set; }

        public int Draws { get; set; }

        public int Loses { get; set; }
    }
}
