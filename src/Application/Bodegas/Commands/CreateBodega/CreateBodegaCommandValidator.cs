
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Interfaces;
using seminario.Domain.Entities;

namespace seminario.Application.Bodegas.Commands.CreateBodega;
public class CreateBodegaCommandValidator : AbstractValidator<CreateBodegaCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateBodegaCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.TipoBodega)
            .NotNull().WithMessage("El campo tipoBodega es obligatorio.");

        RuleFor(v => v.Descripcion)
            .NotNull().WithMessage("El campo descripcion es obligatorio.")
            .NotEmpty().WithMessage("El campo descripcion no puede estar vacio.")
            .MaximumLength(Bodega.MAX_DESCRIPCION_LENGTH).WithMessage($"El campo descripcion puede tener {Planta.MAX_DESCRIPCION_LENGTH} caracteres como maximo.");

        RuleFor(v => v.Detalle)
            .MaximumLength(Bodega.MAX_DETALLE_LENGTH).WithMessage($"El campo detalle puede tener {Planta.MAX_DETALLE_LENGTH} caracteres como maximo.");

        RuleFor(v => v.Direccion)
            .NotNull().WithMessage("El campo direccion es obligatorio.")
            .NotEmpty().WithMessage("El campo direccion no puede estar vacio.")
            .MaximumLength(Ubicacion.MAX_DIRECCION_LENGTH).WithMessage($"El campo direccion puede tener {Ubicacion.MAX_DIRECCION_LENGTH} caracteres como maximo.");

        RuleFor(v => v.MunicipioId)
            .NotNull().WithMessage("El campo municipioId es obligatorio.")
            .MustAsync(MunicipioExists).WithMessage($"No existe un municipio con el id proporcionado.");

        RuleFor(v => v.EncargadoId)
            .NotNull().WithMessage("El campo encargadoId es obligatorio.")
            .MustAsync(EncargadoExists).WithMessage($"No existe un usuario con el encargadoId proporcionado.");

    }

    public async Task<bool> MunicipioExists(int? municipioId, CancellationToken cancellationToken)
    {
        return await _context.Municipio.AnyAsync(m => m.Id == municipioId);
    }

    public async Task<bool> EncargadoExists(string encargadoId, CancellationToken cancellationToken)
    {
        return await _context.ApplicationUser.AnyAsync(u => u.Id == encargadoId);
    }

}
