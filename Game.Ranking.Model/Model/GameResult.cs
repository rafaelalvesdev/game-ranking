namespace Game.Ranking.Model
{
    public class GameResult : ReplicableObject
    {
        public long PlayerID { get; set; }
        public long GameID { get; set; }
        public long WinPoints { get; set; }
        public long GameTimestamp { get; set; }
    }
}