using FluentValidation;
using seminario.Domain.Entities;

namespace seminario.Application.Vehiculos.Commands.UpdateVehiculo;

public class UpdateVehiculoCommandValidator : AbstractValidator<UpdateVehiculoCommand>
{
    public UpdateVehiculoCommandValidator()
    {

        RuleFor(v => v.VehiculoId)
            .GreaterThan(0).WithMessage("El campo vehiculoId debe contener un entero positivo.");

        RuleFor(v => v.EsUsoInterno)
            .NotNull().WithMessage("El campo esUsoInterno es obligatorio.");

        RuleFor(v => v.Codigo)
            .NotNull().WithMessage("El campo codigo es obligatorio.")
            .NotEmpty().WithMessage("El campo codigo es obligatorio.")
            .MaximumLength(Vehiculo.MAX_CODIGO_LENGTH).WithMessage($"El maximo de caracteres para el campo codigo es de {Vehiculo.MAX_CODIGO_LENGTH}.");

        RuleFor(v => v.Placa)
            .NotNull().WithMessage("El campo placa es obligatorio.")
            .NotEmpty().WithMessage("El campo placa es obligatorio.")
            .MaximumLength(Vehiculo.MAX_PLACA_LENGTH).WithMessage($"El maximo de caracteres para el campo placa es de {Vehiculo.MAX_PLACA_LENGTH}.");

        RuleFor(v => v.Descripcion)
            .NotNull().WithMessage("El campo descripcion es obligatorio.")
            .NotEmpty().WithMessage("El campo descripcion es obligatorio.")
            .MaximumLength(Vehiculo.MAX_DESCRIPCION_LENGTH).WithMessage($"El maximo de caracteres para el campo descripcion es de {Vehiculo.MAX_DESCRIPCION_LENGTH}.");

        RuleFor(v => v.Codigo)
            .NotNull().WithMessage("El campo detalle es obligatorio.")
            .NotEmpty().WithMessage("El campo detalle es obligatorio.")
            .MaximumLength(Vehiculo.MAX_DETALLE_LENGTH).WithMessage($"El maximo de caracteres para el campo detalle es de {Vehiculo.MAX_DETALLE_LENGTH}.");

        RuleFor(v => v.CapacidadCarga)
            .NotNull().WithMessage("El campo capacidadCarga es obligatorio.")
            .GreaterThanOrEqualTo(0).WithMessage("El campo capacidadCarga debe ser un numero positivo.");
    }
}
