using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Interfaces;
using seminario.Application.Common.Models;

namespace seminario.Application.Vehiculos.Queries.GetVehiculos;
public record GetVehiculosQuery : IRequest<PaginatedList<VehiculoDto>>
{
    public string? Descripcion { get; init; }
    public string? Codigo { get; init; }
    public string? Placa { get; init; }
    public string? Status { get; init; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetVehiculosQueryHandler : IRequestHandler<GetVehiculosQuery, PaginatedList<VehiculoDto>>
{
    private IApplicationDbContext _context;
    private IMapper _mapper;

    public GetVehiculosQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<VehiculoDto>> Handle(GetVehiculosQuery request, CancellationToken cancellationToken)
    {
        var descripcionLike = "%" + request.Descripcion?.Replace(" ", "%")?.ToUpper() + "%";
        return await PaginatedList<VehiculoDto>.CreateAsync(
            _context.Vehiculos
            .Where(v => v.Status != "X" && (request.Descripcion == null || EF.Functions.Like((v.Descripcion + " " + v.Detalle).ToUpper(), descripcionLike))
                && (request.Codigo == null || v.Codigo == request.Codigo)
                && (request.Placa == null || v.Placa == request.Placa)
                && (request.Status == null || v.Status == request.Status))
            .OrderBy(v => v.Id)
            .ProjectTo<VehiculoDto>(_mapper.ConfigurationProvider)
            , request.PageNumber, request.PageSize);
    }
}