using Nest;
using System;

namespace Game.Ranking.Model
{
    [ElasticsearchType(IdProperty = nameof(PlayerID))]
    public class LeaderboardItem
    {
        public long PlayerID { get; set; }
        public long Balance { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}