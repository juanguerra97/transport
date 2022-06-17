using FluentValidation;
using seminario.Domain.Entities;

namespace seminario.Application.PedidoMateriales.Commands.UpdatePedidoMaterialCommand;
public class UpdatePedidoMaterialCommandValidator : AbstractValidator<UpdatePedidoMaterialCommand>
{

    public UpdatePedidoMaterialCommandValidator()
    {
        RuleFor(v => v.PedidoMaterialId)
            .NotNull().WithMessage("El campo pedidoMaterialId es obligatorio.")
            .GreaterThan(0).WithMessage("El valor del campo pedidoMaterialId debe ser un entero positivo.");

        RuleFor(v => v.Detalle)
            .NotNull().WithMessage("El campo detalle es obligatorio.")
            .MaximumLength(PedidoMaterial.MAX_DETALLE_LENGTH).WithMessage($"El maximo de caracteres permitido para el campo detalle es {PedidoMaterial.MAX_DETALLE_LENGTH}.");

        RuleFor(v => v.Cantidad)
            .NotNull().WithMessage("El campo cantidad es obligatorio.")
            .GreaterThan(0).WithMessage("El valor del campo cantidad debe ser un numero positivo.");
    }

}
