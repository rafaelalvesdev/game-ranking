using System;

namespace Game.Ranking.Services.Messages
{
    public class GameWinPointsMessage
    {
        public long GameID { get; set; }
        public long WinPoints { get; set; }
        public DateTime GameTimestamp { get; set; }
    }
}
