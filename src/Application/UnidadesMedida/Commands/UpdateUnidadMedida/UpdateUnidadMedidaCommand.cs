using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Exceptions;
using seminario.Application.Common.Interfaces;
using seminario.Application.UnidadesMedida.Queries;
using seminario.Domain.Entities;

namespace seminario.Application.UnidadesMedida.Commands.UpdateUnidadMedida;
public record UpdateUnidadMedidaCommand : IRequest<UnidadMedidaDto>
{
    public int? UnidadMedidaId { get; set; }

    public string? Descripcion { get; init; }

    public string? DescripcionPlural { get; init; }

    public string? DescripcionCorta { get; init; }
}

public class UpdateUnidadMedidaCommandHandler : IRequestHandler<UpdateUnidadMedidaCommand, UnidadMedidaDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateUnidadMedidaCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UnidadMedidaDto> Handle(UpdateUnidadMedidaCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.UnidadMedida
            .FirstOrDefaultAsync(u => u.Id == request.UnidadMedidaId, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(UnidadMedida), request.UnidadMedidaId);
        }

        entity.Descripcion = request.Descripcion;
        entity.DescripcionCorta = request.DescripcionCorta;
        entity.DescripcionPlural = request.DescripcionPlural;

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<UnidadMedida, UnidadMedidaDto>(entity);
    }
}