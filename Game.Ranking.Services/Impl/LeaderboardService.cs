using Game.Ranking.Model;
using Game.Ranking.Services.Extensions;
using Game.Ranking.Services.Interfaces;
using Game.Ranking.Services.Results;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Game.Ranking.Services.Impl
{
    public class LeaderboardService : ILeaderboardService
    {
        private Infrastructure.Replication.Interfaces.IGameResultRepository ReplicationRepository { get; set; }
        private Infrastructure.InMemory.Interfaces.ILeaderboardItemRepository MemoryRepository { get; set; }
        private const short MaxLeaderboardSize = 1000;

        public LeaderboardService(Infrastructure.Replication.Interfaces.IGameResultRepository replicationRepository, 
                                  Infrastructure.InMemory.Interfaces.ILeaderboardItemRepository memoryRepository)
        {
            ReplicationRepository = replicationRepository;
            MemoryRepository = memoryRepository;
        }

        public async Task<ServiceResult> GetLeaderboard(int? topRecords = null)
        {
            topRecords = Math.Max(topRecords ?? 100, MaxLeaderboardSize);

            while (!Monitor.TryEnter(MemoryRepository.LockObject)) Thread.Sleep(10);

            var cacheList = MemoryRepository.Get().ToList();
            if (cacheList.Any())
                return cacheList.OrderByDescending(x => x.Balance)
                    .Take(topRecords.Value).AsServiceResult().Valid();
                        
            lock (MemoryRepository.LockObject)
            {
                var loadResult = LoadLeaderboard() as ServiceResult<List<LeaderboardItem>>;
                if (loadResult.IsValid)
                {
                    MemoryRepository.Create(loadResult.Data);
                    cacheList = loadResult.Data.Take(topRecords.Value).ToList();
                }
            }

            return cacheList.AsServiceResult().Valid();
        }

        public void InvalidateLeaderboardCache()
        {
            if (Monitor.TryEnter(MemoryRepository.LockObject))
                lock (MemoryRepository.LockObject)
                    MemoryRepository.DeleteAll();
        }

        private ServiceResult LoadLeaderboard()
        {            
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
                return searchResponse.AsServiceResult().Invalid();

            var leaderboardList = searchResponse.Aggregations.Terms(PlayerAggs).Buckets.Select(x => new LeaderboardItem()
            {
                PlayerID = Convert.ToInt64(x.Key),
                Balance = Convert.ToInt64(x.Sum(PointsSum).Value),
                LastUpdateDate = new DateTime(Convert.ToInt64(x.Max(LastUpdatedDate).Value)),
            });

            return leaderboardList.AsServiceResult().Valid();
        }

    }
}
