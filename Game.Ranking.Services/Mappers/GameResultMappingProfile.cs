using Game.Ranking.Model;
using Game.Ranking.Services.Mappers.Resolvers;
using Game.Ranking.Services.Messages;
using System.Collections.Generic;

namespace Game.Ranking.Services.Mappers
{
    public class GameResultMappingProfile : AutoMapper.Profile
    {
        public GameResultMappingProfile()
        {
            CreateMap<SaveGameResultMessage, List<GameResult>>()
                .ConvertUsing<SaveGameResultConverter>();                
        }
    }
}
