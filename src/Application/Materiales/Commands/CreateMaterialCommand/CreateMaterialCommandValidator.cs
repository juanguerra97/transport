using FluentValidation;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Interfaces;
using seminario.Domain.Entities;

namespace seminario.Application.Materiales.Commands.CreateMaterialCommand;
public class CreateMaterialCommandValidator : AbstractValidator<CreateMaterialCommand>
{
    private IApplicationDbContext _context;

    public CreateMaterialCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Descripcion)
            .NotNull().WithMessage("El campo descripcion es obligatorio.")
            .NotEmpty().WithMessage("El campo descripcion no puede estar vacio.")
            .MaximumLength(Material.MAX_DESCRIPCION_LENGTH).WithMessage($"El campo descripcion puede tener {Material.MAX_DESCRIPCION_LENGTH} caracteres como maximo.");

        RuleFor(v => v.Detalle)
            .MaximumLength(Material.MAX_DETALLE_LENGTH).WithMessage($"El campo detalle puede tener {Material.MAX_DETALLE_LENGTH} caracteres como maximo.");

        RuleFor(v => v.Peso)
            .NotNull().WithMessage("El campo peso es obligatorio.")
            .GreaterThan(0).WithMessage("El campo peso debe ser un numero positivo.");

        RuleFor(v => v.TipoMaterialId)
            .NotNull().WithMessage("El campo tipoMaterialId es obligatorio.")
            .MustAsync(TipoMaterialExists).WithMessage($"No existe un tipo de material con el id proporcionado.");

        RuleFor(v => v.UnidadMedidaId)
            .NotNull().WithMessage("El campo unidadMedidaId es obligatorio.")
            .MustAsync(UnidadMedidaExists).WithMessage($"No existe una unidad de medida con el id proporcionado.");
    }

    public async Task<bool> TipoMaterialExists(int? tipoMaterialId, CancellationToken cancellationToken)
    {
        return await _context.TipoMaterial.AnyAsync(m => m.Id == tipoMaterialId, cancellationToken);
    }

    public async Task<bool> UnidadMedidaExists(int? unidadMedidaId, CancellationToken cancellationToken)
    {
        return await _context.UnidadMedida.AnyAsync(m => m.Id == unidadMedidaId, cancellationToken);
    }
}
