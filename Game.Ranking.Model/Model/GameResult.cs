using Nest;

namespace Game.Ranking.Model
{
    [ElasticsearchType(IdProperty = nameof(Id))]
    public class GameResult : ReplicableObject
    {
        public string Id { get => $"P{PlayerID}_G{GameID}"; set { } }
        public long PlayerID { get; set; }
        public long GameID { get; set; }
        public long WinPoints { get; set; }
        public long GameTimestamp { get; set; }
    }
}