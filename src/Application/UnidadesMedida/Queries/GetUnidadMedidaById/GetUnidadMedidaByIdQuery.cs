using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Exceptions;
using seminario.Application.Common.Interfaces;
using seminario.Domain.Entities;

namespace seminario.Application.UnidadesMedida.Queries.GetUnidadMedidaById;
public record GetUnidadMedidaByIdQuery : IRequest<UnidadMedidaDto>
{
    public int UnidadMedidaId { get; init; }
}

public class GetUnidadMedidaByIdQueryHandler : IRequestHandler<GetUnidadMedidaByIdQuery, UnidadMedidaDto>
{
    private IApplicationDbContext _context;
    private IMapper _mapper;

    public GetUnidadMedidaByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UnidadMedidaDto> Handle(GetUnidadMedidaByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.UnidadMedidas
            .FirstOrDefaultAsync(p => p.Id == request.UnidadMedidaId, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(UnidadMedida), request.UnidadMedidaId);
        }

        return _mapper.Map<UnidadMedida, UnidadMedidaDto>(entity);
    }
}