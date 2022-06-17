using FluentValidation;
using seminario.Domain.Entities;

namespace seminario.Application.PedidoMateriales.Commands.CreatePedidoMaterialCommand;
public class CreatePedidoMaterialCommandValidator : AbstractValidator<CreatePedidoMaterialCommand>
{

    public CreatePedidoMaterialCommandValidator()
    {
        RuleFor(v => v.BodegaSolicitaId)
            .NotNull().WithMessage("El campo bodegaSolicitaId es obligatorio.")
            .GreaterThan(0).WithMessage("El valor del campo bodegaSolicitaId debe ser un entero positivo.");

        RuleFor(v => v.MaterialId)
            .NotNull().WithMessage("El campo materialId es obligatorio.")
            .GreaterThan(0).WithMessage("El valor del campo materialId debe ser un entero positivo.");

        RuleFor(v => v.Detalle)
            .NotNull().WithMessage("El campo detalle es obligatorio.")
            .MaximumLength(PedidoMaterial.MAX_DETALLE_LENGTH).WithMessage($"El maximo de caracteres permitido para el campo detalle es {PedidoMaterial.MAX_DETALLE_LENGTH}.");

        RuleFor(v => v.Cantidad)
            .NotNull().WithMessage("El campo cantidad es obligatorio.")
            .GreaterThan(0).WithMessage("El valor del campo cantidad debe ser un numero positivo.");
    }

}
