using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Constants;
using seminario.Application.Common.Exceptions;
using seminario.Application.Common.Interfaces;
using seminario.Application.PedidoMateriales.Queries;
using seminario.Domain.Entities;

namespace seminario.Application.PedidoMateriales.Commands.EnviarPedidoMaterialCommand;

public record EnviarPedidoMaterialCommand : IRequest<PedidoMaterialDto>
{
    public int PedidoMaterialId { get; init; }
}

public class EnviarPedidoMaterialCommandHandler : IRequestHandler<EnviarPedidoMaterialCommand, PedidoMaterialDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public EnviarPedidoMaterialCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PedidoMaterialDto> Handle(EnviarPedidoMaterialCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.PedidoMateriales
            .Include(pm => pm.EstadoPedidoMaterial)
            .Include(pm => pm.Material)
            .ThenInclude(pm => pm.UnidadMedida)
            .FirstOrDefaultAsync(pm => pm.Id == request.PedidoMaterialId && pm.Status == "A", cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(PedidoMaterial), request.PedidoMaterialId);
        }

        if (entity.EstadoPedidoMaterialId != EstadosPedidoMaterialConstants.CREADO.Id)
        {
            throw new CustomValidationException($"El pedido esta en estado {entity.EstadoPedidoMaterial.Descripcion} y no puede ser enviado.");
        }

        entity.EstadoPedidoMaterialId = EstadosPedidoMaterialConstants.PENDIENTE.Id;
        await _context.BitacoraEstadoPedidoMateriales.AddAsync(new BitacoraEstadoPedidoMaterial
        {
            PedidoMaterial = entity,
            EstadoPedidoMaterialId = EstadosPedidoMaterialConstants.PENDIENTE.Id
        }, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<PedidoMaterial, PedidoMaterialDto>(entity);

    }
}
