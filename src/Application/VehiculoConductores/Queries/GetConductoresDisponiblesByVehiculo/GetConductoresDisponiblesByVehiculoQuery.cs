using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Interfaces;
using seminario.Application.Conductores.Queries;

namespace seminario.Application.VehiculoConductores.Queries.GetConductoresDisponiblesByVehiculo;

public record GetConductoresDisponiblesByVehiculoQuery : IRequest<List<ConductorDto>>
{
    public int VehiculoId { get; init; }
}

public class GetConductoresDisponiblesVehiculoQueryHandler : IRequestHandler<GetConductoresDisponiblesByVehiculoQuery, List<ConductorDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetConductoresDisponiblesVehiculoQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ConductorDto>> Handle(GetConductoresDisponiblesByVehiculoQuery request, CancellationToken cancellationToken)
    {
        var conductoresAgregados = _context.VehiculoConductor
            .Where(vc => vc.VehiculoId == request.VehiculoId && vc.Status == "A")
            .Select(vc => vc.ConductorId);

        return await _context.Conductor
            .Where(c => !conductoresAgregados.Contains(c.Id))
            .ProjectTo<ConductorDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
