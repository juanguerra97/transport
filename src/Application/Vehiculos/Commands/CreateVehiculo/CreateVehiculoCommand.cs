using MediatR;
using seminario.Application.Common.Interfaces;
using seminario.Domain.Entities;

namespace seminario.Application.Vehiculos.Commands.CreateVehiculo;

public record CreateVehiculoCommand : IRequest<int?>
{
    public bool EsUsoInterno { get; set; } = true;
    public string? Codigo { get; init; }
    public string? Placa { get; init; }
    public string? Descripcion { get; init; }
    public string? Detalle { get; init; }
    public double? CapacidadCarga { get; init; }
}

public class CreateVehiculoCommandHandler : IRequestHandler<CreateVehiculoCommand, int?>
{
    private readonly IApplicationDbContext _context;

    public CreateVehiculoCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<int?> Handle(CreateVehiculoCommand request, CancellationToken cancellationToken)
    {
        var entity = new Vehiculo
        {
            EsUsoInterno = request.EsUsoInterno,
            Codigo = request.Codigo,
            Placa = request.Placa,
            Descripcion = request.Descripcion,
            Detalle = request.Detalle,
            CapacidadCarga = request.CapacidadCarga,
        };
        await _context.Vehiculos.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}