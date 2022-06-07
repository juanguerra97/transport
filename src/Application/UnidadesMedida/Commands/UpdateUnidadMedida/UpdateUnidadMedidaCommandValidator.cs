using FluentValidation;
using seminario.Domain.Entities;

namespace seminario.Application.UnidadesMedida.Commands.UpdateUnidadMedida;
public class UpdateUnidadMedidaCommandValidator : AbstractValidator<UpdateUnidadMedidaCommand>
{
    public UpdateUnidadMedidaCommandValidator()
    {
        RuleFor(v => v.UnidadMedidaId)
            .NotNull().WithMessage("El campo unidadMedidaId es obligatorio.");

        RuleFor(v => v.Descripcion)
            .NotNull().WithMessage("El campo descripcion es obligatorio.")
            .NotEmpty().WithMessage("El campo descripcion no puede estar vacio.")
            .MaximumLength(UnidadMedida.MAX_DESCRIPCION_LENGTH).WithMessage($"El campo descripcion puede tener {UnidadMedida.MAX_DESCRIPCION_LENGTH} caracteres como maximo.");

        RuleFor(v => v.DescripcionCorta)
            .NotNull().WithMessage("El campo descripcionCorta es obligatorio.")
            .NotEmpty().WithMessage("El campo descripcionCorta no puede estar vacio.")
            .MaximumLength(UnidadMedida.MAX_DESCRIPCIONCORTA_LENGTH).WithMessage($"El campo descripcionCorta puede tener {UnidadMedida.MAX_DESCRIPCIONCORTA_LENGTH} caracteres como maximo.");

        RuleFor(v => v.DescripcionPlural)
            .NotNull().WithMessage("El campo descripcionPlural es obligatorio.")
            .NotEmpty().WithMessage("El campo descripcionPlural no puede estar vacio.")
            .MaximumLength(UnidadMedida.MAX_DESCRIPCIONPLURAL_LENGTH).WithMessage($"El campo descripcionPlural puede tener {UnidadMedida.MAX_DESCRIPCIONPLURAL_LENGTH} caracteres como maximo.");
    }
}