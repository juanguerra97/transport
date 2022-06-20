using FluentValidation;
using seminario.Domain.Entities;

namespace seminario.Application.Conductores.Commands.CreateConductor;

public class CreateConductorCommandValidator : AbstractValidator<CreateConductorCommand>
{
    public CreateConductorCommandValidator()
    {
        RuleFor(v => v.UserId)
            .NotNull().WithMessage("El campo userId es obligatorio.")
            .NotEmpty().WithMessage("El campo userId es obligatorio.")
            .MaximumLength(255).WithMessage("Se permite un maximo de 255 caracteres para el campo userId.");

        RuleFor(v => v.NoLicencia)
            .NotNull().WithMessage("El campo noLicencia es obligatorio.")
            .NotEmpty().WithMessage("El campo noLicencia es obligatorio.")
            .MaximumLength(Conductor.MAX_NOLICENCIA_LENGTH).WithMessage($"Se permite un maximo de {Conductor.MAX_NOLICENCIA_LENGTH} caracteres para el campo noLicencia.");
    }
}
