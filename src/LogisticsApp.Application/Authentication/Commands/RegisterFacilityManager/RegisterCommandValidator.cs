using FluentValidation;
using LogisticsApp.Application.Authentication.Commands.RegisterFacilityWorker;

namespace LogisticsApp.Application.Authentication.Commands.RegisterFacilityManager;

public class RegisterFacilityManagerCommandValidator : AbstractValidator<RegisterFacilityManagerCommand>
{
    public RegisterFacilityManagerCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6);
    }
}
