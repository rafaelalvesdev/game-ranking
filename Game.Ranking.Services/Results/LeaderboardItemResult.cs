using System;

namespace Game.Ranking.Services.Results
{
    public class LeaderboardItemResult
    {
        public long PlayerID { get; set; }
        public long Balance { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}
