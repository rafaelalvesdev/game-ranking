using Game.Ranking.Model;
using Game.Ranking.Services.Results;
using System;

namespace Game.Ranking.Services.Mappers
{
    public class LeaderboardMappingProfile : AutoMapper.Profile
    {
        public LeaderboardMappingProfile()
        {
            CreateMap<LeaderboardItem, LeaderboardItemResult>()
                .ForMember(x => x.LastUpdateDate, opt => opt.ResolveUsing(src => new DateTime(src.LastUpdateDate)));
        }
    }
}
