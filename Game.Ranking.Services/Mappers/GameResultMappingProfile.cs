using Game.Ranking.Model;
using Game.Ranking.Services.Messages;

namespace Game.Ranking.Services.Mappers
{
    public class GameResultMappingProfile : AutoMapper.Profile
    {
        public GameResultMappingProfile()
        {
            CreateMap<SaveGameResultMessage, GameResult>();
        }
    }
}
