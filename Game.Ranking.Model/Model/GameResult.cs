using System;

namespace Game.Ranking.Model
{
    public class GameResult
    {
        public GameResult()
        {
            SaveTimestamp = DateTime.Now.Ticks;
        }

        public long PlayerID { get; set; }
        public long GameID { get; set; }
        public long WinPoints { get; set; }
        public long GameTimestamp { get; set; }
        public long SaveTimestamp { get; set; }
    }
}