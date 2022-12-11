using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FootballPlayers.Services;

public abstract class ServiceBase
{
    protected readonly IServiceProvider ioc;

    public ServiceBase(IServiceProvider ioc)
    {
        this.ioc = ioc;
    }

    protected void ValidateModel<TModel>(TModel model)
    {
        var validator = ioc.GetRequiredService<IValidator<TModel>>();
        var result = validator.Validate(model);
        if (!result.IsValid)
        {
            string message = string.Join(" - ", result.Errors.Select(e => e.ErrorMessage));
            throw new ApplicationException(message);
        }
    }
}