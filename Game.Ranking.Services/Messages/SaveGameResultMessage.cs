using System.Collections.Generic;

namespace Game.Ranking.Services.Messages
{
    public class SaveGameResultMessage
    {
        /// <summary>
        /// ID do jogador
        /// </summary>
        public long PlayerID { get; set; }

        /// <summary>
        /// Coleção de pontos referentes a partidas jogadas por este jogador
        /// </summary>
        public List<GameWinPointsMessage> gameWinPoints { get; set; } = new List<GameWinPointsMessage>();
    }
}
