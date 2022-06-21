using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Interfaces;
using seminario.Application.Vehiculos.Queries;

namespace seminario.Application.VehiculoConductores.Queries.GetVehiculosDisponiblesByConductor;

public record GetVehiculosDisponiblesByConductorQuery : IRequest<List<VehiculoDto>>
{
    public int ConductorId { get; init; }
}

public class GetVehiculosDisponiblesByConductorQueryHandler : IRequestHandler<GetVehiculosDisponiblesByConductorQuery, List<VehiculoDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetVehiculosDisponiblesByConductorQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<VehiculoDto>> Handle(GetVehiculosDisponiblesByConductorQuery request, CancellationToken cancellationToken)
    {
        var vehiculosAgregados = _context.VehiculoConductores
            .Where(vc => vc.ConductorId == request.ConductorId && vc.Status == "A")
            .Select(vc => vc.VehiculoId);

        return await _context.Vehiculos
            .Where(v => !vehiculosAgregados.Contains(v.Id))
            .ProjectTo<VehiculoDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}