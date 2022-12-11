using FluentValidation;

namespace FootballPlayers.Models
{
    public record CreateEditPlayerModel(string Name, int Age, float Height, int TeamId);

    public class CreatePlayerModelValidator : AbstractValidator<CreateEditPlayerModel>
    {
        public CreatePlayerModelValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Age).NotEmpty().GreaterThan(10);
            RuleFor(p => p.Height).NotEmpty().GreaterThan(1).LessThan(2.5f);
            RuleFor(p => p.TeamId).NotEmpty();
        }
    }
}