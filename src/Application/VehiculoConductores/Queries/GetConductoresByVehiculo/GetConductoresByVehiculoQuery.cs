using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Interfaces;
using seminario.Application.Conductores.Queries;

namespace seminario.Application.VehiculoConductores.Queries.GetConductoresByVehiculo;

public record GetConductoresByVehiculoQuery : IRequest<List<ConductorDto>>
{
    public int VehiculoId { get; init; }
}

public class GetConductoresByVehiculoQueryHandler : IRequestHandler<GetConductoresByVehiculoQuery, List<ConductorDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetConductoresByVehiculoQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ConductorDto>> Handle(GetConductoresByVehiculoQuery request, CancellationToken cancellationToken)
    {
        return await _context.VehiculoConductores
            .Where(vc => vc.VehiculoId == request.VehiculoId && vc.Status == "A")
            .Select(vc => vc.Conductor)
            .ProjectTo<ConductorDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
