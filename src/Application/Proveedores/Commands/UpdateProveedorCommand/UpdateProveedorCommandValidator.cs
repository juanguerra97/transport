using FluentValidation;
using seminario.Domain.Entities;

namespace seminario.Application.Proveedores.Commands.UpdateProveedorCommand;
public class UpdateProveedorCommandValidator : AbstractValidator<UpdateProveedorCommand>
{
    public UpdateProveedorCommandValidator()
    {
        RuleFor(v => v.ProveedorId)
            .NotNull().WithMessage("El campo proveedorId es obligatorio.");

        RuleFor(v => v.Nombre)
            .NotNull().WithMessage("El campo nombre es obligatorio.")
            .NotEmpty().WithMessage("el campo nombre es obligatorio.")
            .MaximumLength(ProveedorMaterial.MAX_NOMBRE_LENGTH).WithMessage($"El maximo de caracteres permitido para el nombre es de {ProveedorMaterial.MAX_NOMBRE_LENGTH}.");

        RuleFor(v => v.Nit)
            .NotNull().WithMessage("El campo nit es obligatorio.")
            .NotEmpty().WithMessage("el campo nit es obligatorio.")
            .MaximumLength(ProveedorMaterial.MAX_NIT_LENGTH).WithMessage($"El maximo de caracteres permitido para el nit es de {ProveedorMaterial.MAX_NIT_LENGTH}.");

        RuleFor(v => v.Telefono)
            .NotNull().WithMessage("El campo telefono es obligatorio.")
            .NotEmpty().WithMessage("el campo telefono es obligatorio.")
            .MaximumLength(ProveedorMaterial.MAX_TELEFONO_LENGTH).WithMessage($"El maximo de caracteres permitido para el telefono es de {ProveedorMaterial.MAX_TELEFONO_LENGTH}.");

        RuleFor(v => v.Email)
            .NotNull().WithMessage("El campo email es obligatorio.")
            .NotEmpty().WithMessage("el campo email es obligatorio.")
            .MaximumLength(ProveedorMaterial.MAX_EMAIL_LENGTH).WithMessage($"El maximo de caracteres permitido para el email es de {ProveedorMaterial.MAX_EMAIL_LENGTH}.");

        RuleFor(v => v.Direccion)
            .NotNull().WithMessage("El campo direccion es obligatorio.")
            .NotEmpty().WithMessage("el campo direccion es obligatorio.")
            .MaximumLength(ProveedorMaterial.MAX_DIRECCION_LENGTH).WithMessage($"El maximo de caracteres permitido para la direccion es de {ProveedorMaterial.MAX_DIRECCION_LENGTH}.");

    }
}
