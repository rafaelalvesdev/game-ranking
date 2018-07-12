using Game.Ranking.Infrastructure.Replication.Interfaces;
using Game.Ranking.Model;
using Game.Ranking.Services.Extensions;
using Game.Ranking.Services.Interfaces;
using Game.Ranking.Services.Results;
using Microsoft.Extensions.Caching.Memory;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game.Ranking.Services.Impl
{
    public class LeaderboardService : ILeaderboardService
    {
        private IGameResultRepository ReplicationRepository { get; set; }
        private IMemoryCache Cache { get; set; }

        private const short MaxLeaderboardSize = 1000;
        private const string LeaderboardCacheKey = "leaderboard";
        private const short LeaderboardCacheExpiration = 1;

        public LeaderboardService(IGameResultRepository replicationRepository, IMemoryCache cache)
        {
            ReplicationRepository = replicationRepository;
            Cache = cache;
        }

        public async Task<ServiceResult> GetLeaderboard(int? topRecords = null)
        {
            topRecords = Math.Max(topRecords ?? 100, MaxLeaderboardSize);

            var list = Cache.GetOrCreate<List<LeaderboardItem>>(LeaderboardCacheKey,
                context =>
                {
                    context.SetAbsoluteExpiration(TimeSpan.FromMilliseconds(1));

                    const string PlayerAggs = "playeraggs";
                    const string LastUpdatedDate = "lastupdateddate";
                    const string PointsSum = "pointssum";
                    const string SortByPointsSum = "sortbypointssum";

                    var searchResponse =
                    ReplicationRepository.Search(s => s
                        .Index(ReplicationRepository.IndexName)
                        .Size(MaxLeaderboardSize)
                        .Aggregations(a1 => a1
                            .Terms(PlayerAggs, t1 => t1
                                .Field(f1 => f1.PlayerID)
                                .Size(int.MaxValue)
                                .Aggregations(a2 => a2
                                    .Max(LastUpdatedDate, m2 => m2
                                        .Field(fm2 => fm2.ReplicatedTimestamp)
                                    )
                                    .Sum(PointsSum, s2 => s2
                                        .Field(f2 => f2.WinPoints
                                        )
                                    )
                                    .BucketSort(SortByPointsSum, t3 => t3
                                        .Sort(s3 => s3
                                            .Field(PointsSum, SortOrder.Descending)
                                        )
                                    )
                                )
                            )
                        )
                    );

                    if (!searchResponse.IsValid)
                        return null;

                    context.SetAbsoluteExpiration(TimeSpan.FromMinutes(LeaderboardCacheExpiration));
                    context.SetPriority(CacheItemPriority.High);

                    return searchResponse.Aggregations.Terms(PlayerAggs).Buckets.Select(x => new LeaderboardItem()
                    {
                        PlayerID = Convert.ToInt64(x.Key),
                        Balance = Convert.ToInt64(x.Sum(PointsSum).Value),
                        LastUpdateDate = new DateTime(Convert.ToInt64(x.Max(LastUpdatedDate).Value)),
                    }).ToList();
                });

            return list.Take(topRecords.Value).ToList().AsServiceResult().Valid();
        }
    }
}
