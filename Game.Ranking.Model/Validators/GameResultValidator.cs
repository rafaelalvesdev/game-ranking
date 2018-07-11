using FluentValidation;
using Game.Ranking.Model.Validators.Interfaces;
using Game.Ranking.Resources;

namespace Game.Ranking.Model.Validators
{
    public class GameResultValidator : AbstractValidator<GameResult>, IGameResultValidator
    {
        public GameResultValidator()
        {
            RuleFor(x => x.PlayerID)
                .GreaterThan(0)
                .WithMessage(ValidationResources.PlayerID_MustBeGreatherThanZero);

            RuleFor(x => x.GameID)
                .GreaterThan(0)
                .WithMessage(ValidationResources.GameID_MustBeGreatherThanZero);

            RuleFor(x => x.GameTimestamp)
                .NotEmpty()
                .WithMessage(ValidationResources.Timestamp_MustBeSpecified);
            }
    }
}
