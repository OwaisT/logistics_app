using FluentValidation;

namespace LogisticsApp.Application.Authentication.Commands.RegisterFacilityWorker;

public class RegisterFacilityWorkerCommandValidator : AbstractValidator<RegisterFacilityWorkerCommand>
{
    public RegisterFacilityWorkerCommandValidator()
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
