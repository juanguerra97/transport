using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Bodegas.Queries;
using seminario.Application.Common.Exceptions;
using seminario.Application.Common.Interfaces;
using seminario.Domain.Entities;
using seminario.Domain.Enums;

namespace seminario.Application.Bodegas.Commands.UpdateBodega;
public record UpdateBodegaCommand : IRequest<BodegaDto>
{
    public int? BodegaId { get; set; }
    public TipoBodega TipoBodega { get; init; }
    public string? Descripcion { get; init; }
    public string? Detalle { get; init; }
    public int? MunicipioId { get; init; }
    public string? Direccion { get; init; }
}

public class UpdateBodegaCommandHandler : IRequestHandler<UpdateBodegaCommand, BodegaDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateBodegaCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BodegaDto> Handle(UpdateBodegaCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Bodegas
            .Include(b => b.Ubicacion)
            .ThenInclude(u => u.Municipio)
            .ThenInclude(m => m.Departamento)
            .ThenInclude(d => d.Pais)
            .FirstOrDefaultAsync(p => p.Id == request.BodegaId, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Bodega), request.BodegaId);
        }

        entity.TipoBodega = request.TipoBodega;

        entity.Descripcion = request.Descripcion;
        entity.Ubicacion.Descripcion = request.Descripcion;

        entity.Detalle = request.Detalle;
        entity.Ubicacion.Detalle = request.Detalle;

        entity.Ubicacion.Direccion = request.Direccion;
        entity.Ubicacion.MunicipioId = request.MunicipioId;

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<Bodega, BodegaDto>(entity);
    }
}