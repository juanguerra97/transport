using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Exceptions;
using seminario.Application.Common.Interfaces;
using seminario.Domain.Entities;

namespace seminario.Application.Bodegas.Queries.GetBodegaById;
public record GetBodegaByIdQuery : IRequest<BodegaDto>
{
    public int? BodegaId { get; init; }
}

public class GetBodegaByIdQueryHandler : IRequestHandler<GetBodegaByIdQuery, BodegaDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBodegaByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;

    }
    public async Task<BodegaDto> Handle(GetBodegaByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Bodegas
            .Include(b => b.AdminBodega)
            .ThenInclude(ad => ad.User)
            .Include(b => b.Ubicacion)
            .ThenInclude(u => u.Municipio)
            .ThenInclude(m => m.Departamento)
            .ThenInclude(d => d.Pais)
            .FirstOrDefaultAsync(p => p.Id == request.BodegaId, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(Bodega), request.BodegaId);
        }

        return _mapper.Map<Bodega, BodegaDto>(entity);
    }
}
