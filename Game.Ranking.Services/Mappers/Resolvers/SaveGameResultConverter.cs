using AutoMapper;
using Game.Ranking.Model;
using Game.Ranking.Services.Messages;
using System.Collections.Generic;
using System.Linq;

namespace Game.Ranking.Services.Mappers.Resolvers
{
    public class SaveGameResultConverter : ITypeConverter<SaveGameResultMessage, List<GameResult>>
    {
        public List<GameResult> Convert(SaveGameResultMessage source, List<GameResult> destination, ResolutionContext context)
        {
            return source.gameWinPoints?.Select(x => new GameResult()
            {
                PlayerID = source.PlayerID,
                GameID = x.GameID,
                WinPoints = x.WinPoints,
                GameTimestamp = x.GameTimestamp.Ticks,
            })?.ToList() ?? new List<GameResult>();
        }
    }
}
