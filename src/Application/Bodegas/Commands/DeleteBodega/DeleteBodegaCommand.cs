using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Bodegas.Queries;
using seminario.Application.Common.Exceptions;
using seminario.Application.Common.Interfaces;
using seminario.Domain.Entities;

namespace seminario.Application.Bodegas.Commands.DeleteBodega;
public record DeleteBodegaCommand : IRequest<BodegaDto>
{
    public int? BodegaId { get; init; }
}

public class DeleteBodegaCommandHandler : IRequestHandler<DeleteBodegaCommand, BodegaDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public DeleteBodegaCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BodegaDto> Handle(DeleteBodegaCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Bodega
            .Include(b => b.Ubicacion)
            .ThenInclude(u => u.Municipio)
            .ThenInclude(m => m.Departamento)
            .ThenInclude(d => d.Pais)
            .FirstOrDefaultAsync(p => p.Id == request.BodegaId, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Bodega), request.BodegaId);
        }

        entity.Status = "X";
        entity.Ubicacion.Status = "X";

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<Bodega, BodegaDto>(entity);
    }
}
