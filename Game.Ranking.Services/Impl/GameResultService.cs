using AutoMapper;
using Game.Ranking.Infrastructure.Interfaces;
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
        private IGameResultRepository Repository { get; set; }
        private IGameResultValidator Validator { get; set; }

        public GameResultService(IGameResultRepository repository, IGameResultValidator validator)
        {
            Repository = repository;
            Validator = validator;
        }

        public async Task<ServiceResult> Save(SaveGameResultMessage message)
        {
            var gameResults = Mapper.Map<List<GameResult>>(message);

            var errorMessages = new List<string>();
            var allValid = true;
            gameResults.ForEach(x => {
                var result = Validator.Validate(x);
                errorMessages.AddRange(result.Errors?.Select(e => e.ErrorMessage));
                allValid = allValid && result.IsValid;
            });

            if (!allValid)
                return new ServiceResult().Invalid().WithErrors(errorMessages);

            var response = Repository.IndexBulk(gameResults);
            if (!response.IsValid)
                return response.DebugInformation.AsServiceResult().Invalid();
            else
                return new ServiceResult().Valid();
        }
    }
}