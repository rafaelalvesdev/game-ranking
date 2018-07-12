using AutoMapper;
using Game.Ranking.Infrastructure.InMemory.Interfaces;
using Game.Ranking.Model;
using Game.Ranking.Model.Validators.Interfaces;
using Game.Ranking.Services.Extensions;
using Game.Ranking.Services.Interfaces;
using Game.Ranking.Services.Messages;
using Game.Ranking.Services.Results;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game.Ranking.Services.Impl
{
    public class GameResultService : IGameResultService
    {
        private IGameResultMemoryStorageService MemoryStorageService { get; set; }
        private IGameResultReplicationService ReplicationService { get; set; }
        private IGameResultValidator Validator { get; set; }


        public GameResultService(IGameResultMemoryStorageService msService, IGameResultReplicationService rService, IGameResultValidator validator)
        {
            MemoryStorageService = msService;
            ReplicationService = rService;
            Validator = validator;
        }

        public async Task<ServiceResult> Save(SaveGameResultMessage message)
        {
            var gameResults = Mapper.Map<List<GameResult>>(message);

            var errorMessages = new List<string>();
            var allValid = true;
            gameResults.ForEach(x =>
            {
                var result = Validator.Validate(x);
                errorMessages.AddRange(result.Errors?.Select(e => e.ErrorMessage));
                allValid = allValid && result.IsValid;
            });

            if (!allValid)
                return new ServiceResult().Invalid().WithErrors(errorMessages);

            var response = MemoryStorageService.StoreInMemory(gameResults);
            return response;
        }

        public async Task<ServiceResult> Replicate()
        {
            var mItemsResult = MemoryStorageService.GetFromMemory() as ServiceResult<List<GameResult>>;
            ServiceResult replResult = null;

            if (mItemsResult.IsValid)
                if (mItemsResult.Data.Count() == 0)
                    return new ServiceResult().Valid();
                else
                {
                    replResult = ReplicationService.Replicate(mItemsResult.Data);
                }

            if (replResult.IsValid)
            {
                MemoryStorageService.DeleteFromMemory(mItemsResult.Data);

                return new ServiceResult().Valid();
            }
            else
                return new ServiceResult().Invalid();
        }
    }
}