using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Exceptions;
using seminario.Application.Common.Interfaces;
using seminario.Application.UnidadesMedida.Queries;
using seminario.Domain.Entities;

namespace seminario.Application.UnidadesMedida.Commands.DeleteUnidadMedida;
public record DeleteUnidadMedidaCommand : IRequest<UnidadMedidaDto>
{
    public int? UnidadMedidaId { get; init; }
}

public class DeleteUnidadMedidaCommandHandler : IRequestHandler<DeleteUnidadMedidaCommand, UnidadMedidaDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public DeleteUnidadMedidaCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public  async Task<UnidadMedidaDto> Handle(DeleteUnidadMedidaCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.UnidadMedida
            .FirstOrDefaultAsync(u => u.Id == request.UnidadMedidaId, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(UnidadMedida), request.UnidadMedidaId);
        }

        entity.Status = "X";

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<UnidadMedida, UnidadMedidaDto>(entity);
    }
}