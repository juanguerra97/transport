using MediatR;
using seminario.Application.Common.Interfaces;
using seminario.Domain.Entities;

namespace seminario.Application.Materiales.Commands.CreateMaterialCommand;
public record CreateMaterialCommand : IRequest<int?>
{

    public int? TipoMaterialId { get; set; }

    public string? Descripcion { get; set; }

    public string? Detalle { get; set; }

    public int? UnidadMedidaId { get; set; }

    public double? Peso { get; set; }
}

public class CreateMaterialCommandHandler : IRequestHandler<CreateMaterialCommand, int?>
{
    private readonly IApplicationDbContext _context;

    public CreateMaterialCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int?> Handle(CreateMaterialCommand request, CancellationToken cancellationToken)
    {
        var entity = new Material
        {
            TipoMaterialId = request.TipoMaterialId,
            Descripcion = request.Descripcion,
            Detalle = request.Detalle,
            Peso = request.Peso,
            UnidadMedidaId = request.UnidadMedidaId
        };

        await _context.Materiales.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}