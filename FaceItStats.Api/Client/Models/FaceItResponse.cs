using System.Collections.Generic;

namespace FaceItStats.Api.Client.Models
{

    public class FaceItResponse
    {
        public int elo { get; set; }
        public int lvl { get; set; }
        public string todayEloDiff { get; set; }
        public List<LatestMatch> latestMatches { get; set; }
        public Stats stats { get; set; }
        public string ladder { get; set; }
        public string report { get; set; }
        public string trend { get; set; }
        public string last_match { get; set; }
    }
}
