using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FaceItStats.Api.Persistence.Models
{
    [Table(nameof(MatchResult))]
    public class MatchResult
    {
        [Key]
        public int Id { get; protected set; }

        [Required]
        public string MatchId { get; private set; }

        public bool IsWin { get; private set; }

        public int Kills { get; private set; }

        public decimal KdRatio { get; private set; }

        public string ContestId { get; private set; }

        public string FirstOptionId { get; private set; }

        public string SecondOptionId { get; private set; }

        [Required]
        public bool IsStarted { get; set; }

        [Required]
        public bool IsCancelled { get; set; }

        [Required]
        public bool IsFinished { get; set; }

        [Required]
        public bool IsResultSent { get; private set; }

        public MatchResult()
        {

        }

        public MatchResult(string matchId)
        {
            MatchId = matchId;
            IsWin = false;
            Kills = 0;
            KdRatio = 0;
            IsStarted = false;
            IsCancelled = false;
            IsFinished = false;
            IsResultSent = false;
        }

        public void AddResult(bool isWin, int kills, decimal kdRatio)
        {
            IsWin = isWin;
            Kills = kills;
            KdRatio = kdRatio;
        }

        public void SetContest(string contestId, string firstOptionId, string secondOptionId)
        {
            ContestId = contestId;
            FirstOptionId = firstOptionId;
            SecondOptionId = secondOptionId;
        }

        public void MarkAsStarted() => IsStarted = true;

        public void MarkAsFinished() => IsFinished = true;

        public void MarkAsCancelled() => IsCancelled = true;

        public void MarkAsSent() => IsResultSent = true;
    }
}
