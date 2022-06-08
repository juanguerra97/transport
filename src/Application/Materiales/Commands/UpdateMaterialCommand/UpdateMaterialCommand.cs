using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Exceptions;
using seminario.Application.Common.Interfaces;
using seminario.Application.Materiales.Queries;
using seminario.Domain.Entities;

namespace seminario.Application.Materiales.Commands.UpdateMaterialCommand;
public record UpdateMaterialCommand : IRequest<MaterialDto>
{
    public int? MaterialId { get; set; }
    public int? TipoMaterialId { get; init; }

    public string? Descripcion { get; init; }

    public string? Detalle { get; init; }

    public int? UnidadMedidaId { get; init; }

    public double? Peso { get; init; }
}

public class UpdateMaterialCommandHandler : IRequestHandler<UpdateMaterialCommand, MaterialDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateMaterialCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<MaterialDto> Handle(UpdateMaterialCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Materiales
            .Include(m => m.TipoMaterial)
            .Include(m => m.UnidadMedida)
            .FirstOrDefaultAsync(m => m.Id == request.MaterialId && m.Status == "A", cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Material), request.MaterialId);
        }

        entity.Descripcion = request.Descripcion;
        entity.Detalle = request.Detalle;
        entity.TipoMaterial = await _context.TipoMateriales.FirstOrDefaultAsync(t => t.Id == request.TipoMaterialId && t.Status == "A", cancellationToken);
        entity.UnidadMedida = await _context.UnidadMedidas.FirstOrDefaultAsync(u => u.Id == request.UnidadMedidaId && u.Status == "A", cancellationToken);
        entity.Peso = request.Peso;

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<Material, MaterialDto>(entity);
    }
}
