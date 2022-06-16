using FluentValidation;

namespace seminario.Application.IngresoMateriales.Commands.CreateIngresoMaterialCommand;

public class CreateIngresoMaterialCommandValidator : AbstractValidator<CreateIngresoMaterialCommand>
{
    public CreateIngresoMaterialCommandValidator()
    {
        RuleFor(v => v.ProveedorMaterialId)
            .NotNull().WithMessage("El campo proveedorMaterialId es obligatorio.")
            .GreaterThan(0).WithMessage("El valor de proveedorMaterialId debe ser un entero positivo.");

        RuleFor(v => v.MaterialId)
            .NotNull().WithMessage("El campo materialId es obligatorio.")
            .GreaterThan(0).WithMessage("El valor de materialId debe ser un entero positivo.");

        RuleFor(v => v.BodegaId)
            .NotNull().WithMessage("El campo bodegaId es obligatorio.")
            .GreaterThan(0).WithMessage("El valor de bodegaId debe ser un entero positivo.");

        RuleFor(v => v.Cantidad)
            .NotNull().WithMessage("El campo cantidad es obligatorio.")
            .GreaterThan(0).WithMessage("El valor de cantidad debe ser un numero positivo.");

    }
}
