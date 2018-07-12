using System;

namespace Game.Ranking.Model
{
    public class LeaderboardItem : ReplicableObject
    {
        public long PlayerID { get; set; }
        public long Balance { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}