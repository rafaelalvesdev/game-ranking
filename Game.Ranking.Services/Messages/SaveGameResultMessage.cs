using System.Collections.Generic;

namespace Game.Ranking.Services.Messages
{
    public class SaveGameResultMessage
    {
        public long PlayerID { get; set; }
        public List<GameWinPointsMessage> gameWinPoints { get; set; } = new List<GameWinPointsMessage>();
    }
}
