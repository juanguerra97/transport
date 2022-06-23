using AutoMapper;
using MediatR;
using seminario.Application.Common.Interfaces;
using seminario.Application.Vehiculos.Queries;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace seminario.Application.VehiculoConductores.Queries.GetVehiculosByConductor;

public record GetVehiculosByConductorQuery : IRequest<List<VehiculoDto>>
{
    public int ConductorId { get; init; }
}

public class GetVehiculosByConductorQueryHandler : IRequestHandler<GetVehiculosByConductorQuery, List<VehiculoDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetVehiculosByConductorQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<VehiculoDto>> Handle(GetVehiculosByConductorQuery request, CancellationToken cancellationToken)
    {
        return await _context.VehiculoConductor
            .Where(vc => vc.ConductorId == request.ConductorId && vc.Status == "A")
            .Select(vc => vc.Vehiculo)
            .ProjectTo<VehiculoDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}