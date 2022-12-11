using FluentValidation;

namespace FootballPlayers.Models;

public record TransferPlayerModel(int DestinationTeamId);

public class TransferPlayerModelValidator : AbstractValidator<TransferPlayerModel>
{
    public TransferPlayerModelValidator()
    {
        RuleFor(p => p.DestinationTeamId).NotEmpty().GreaterThan(0);
    }
}