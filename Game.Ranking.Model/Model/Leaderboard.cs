using System;

namespace Game.Ranking.Model
{
    class Leaderboard
    {
        public long PlayerID { get; set; }
        public long Balance { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}