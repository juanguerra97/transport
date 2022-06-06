
using MediatR;
using seminario.Application.Common.Interfaces;
using seminario.Domain.Entities;
using seminario.Domain.Enums;

namespace seminario.Application.Bodegas.Commands.CreateBodega;
public record CreateBodegaCommand : IRequest<int?>
{
    public TipoBodega TipoBodega { get; init; }
    public string? Descripcion { get; init; }
    public string? Detalle { get; init; }
    public int? MunicipioId { get; init; }
    public string? Direccion { get; init; }
}

public class CreateBodegaCommandHandler : IRequestHandler<CreateBodegaCommand, int?>
{
    private readonly IApplicationDbContext _context;

    public CreateBodegaCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int?> Handle(CreateBodegaCommand request, CancellationToken cancellationToken)
    {
        var ubicacion = new Ubicacion
        {
            TipoUbicacion = TipoUbicacion.BODEGA,
            Descripcion = request.Descripcion,
            Detalle = request.Detalle,
            Direccion = request.Direccion,
            MunicipioId = request.MunicipioId,
        };
        _context.Ubicaciones.Add(ubicacion);

        var bodega = new Bodega
        {
            TipoBodega = request.TipoBodega,
            Descripcion = request.Descripcion,
            Detalle = request.Detalle,
            Ubicacion = ubicacion
        };
        _context.Bodegas.Add(bodega);

        await _context.SaveChangesAsync(cancellationToken);

        return bodega.Id;
    }
}