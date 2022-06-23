using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Exceptions;
using seminario.Application.Common.Interfaces;
using seminario.Application.Plantas.Queries;
using seminario.Domain.Entities;

namespace seminario.Application.Plantas.Commands.DeletePlanta;
public record DeletePlantaCommand : IRequest<PlantaDto>
{
    public int? PlantaId { get; init; }
}

public class DeletePlantaCommandHandler : IRequestHandler<DeletePlantaCommand, PlantaDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public DeletePlantaCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PlantaDto> Handle(DeletePlantaCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Planta
            .Include(p => p.TipoPlanta)
            .Include(p => p.Bodega)
            .ThenInclude(b => b.Ubicacion)
            .ThenInclude(u => u.Municipio)
            .ThenInclude(m => m.Departamento)
            .ThenInclude(d => d.Pais)
            .FirstOrDefaultAsync(p => p.Id == request.PlantaId, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Planta), request.PlantaId);
        }

        entity.Status = "X";
        entity.Bodega.Status = "X";
        entity.Bodega.Ubicacion.Status = "X";

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<Planta, PlantaDto>(entity);
    }
}
