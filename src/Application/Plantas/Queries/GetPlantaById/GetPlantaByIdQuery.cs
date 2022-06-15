using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Exceptions;
using seminario.Application.Common.Interfaces;
using seminario.Domain.Entities;

namespace seminario.Application.Plantas.Queries.GetPlantaById;
public record GetPlantaByIdQuery : IRequest<PlantaDto>
{
    public int? PlantaId { get; init; }
}

public class GetPlantaByIdQueryHandler : IRequestHandler<GetPlantaByIdQuery, PlantaDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPlantaByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PlantaDto> Handle(GetPlantaByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Plantas
            .Include(p => p.AdminPlanta)
            .ThenInclude(ad => ad.User)
            .Include(p => p.TipoPlanta)
            .Include(p => p.Bodega)
            .ThenInclude(b => b.Ubicacion)
            .ThenInclude(u => u.Municipio)
            .ThenInclude(m => m.Departamento)
            .ThenInclude(d => d.Pais)
            .FirstOrDefaultAsync(p => p.Id == request.PlantaId, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(Planta), request.PlantaId);
        }

        return _mapper.Map<Planta, PlantaDto>(entity);
    }
}