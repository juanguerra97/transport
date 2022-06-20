using FluentValidation;
using seminario.Domain.Entities;

namespace seminario.Application.Conductores.Commands.UpdateConductor;

public class UpdateConductorCommandValidator : AbstractValidator<UpdateConductorCommand>
{
    public UpdateConductorCommandValidator()
    {
        RuleFor(v => v.NoLicencia)
            .NotNull().WithMessage("El campo noLicencia es obligatorio.")
            .NotEmpty().WithMessage("El campo noLicencia es obligatorio.")
            .MaximumLength(Conductor.MAX_NOLICENCIA_LENGTH).WithMessage($"Se permite un maximo de {Conductor.MAX_NOLICENCIA_LENGTH} caracteres para el campo noLicencia.");
    }
}
