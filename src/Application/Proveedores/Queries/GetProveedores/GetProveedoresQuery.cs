using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Interfaces;
using seminario.Application.Common.Models;

namespace seminario.Application.Proveedores.Queries.GetProveedores;
public record GetProveedoresQuery : IRequest<PaginatedList<ProveedorDto>>
{
    public string? Nombre { get; set; }
    public string? Nit { get; set; }
    public string? Telefono { get; set; }
    public string? Email { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetProveedoresQueryHandler : IRequestHandler<GetProveedoresQuery, PaginatedList<ProveedorDto>>
{

    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProveedoresQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ProveedorDto>> Handle(GetProveedoresQuery request, CancellationToken cancellationToken)
    {

        string? nombreLike = "%" + request.Nombre?.Replace(" ", "%")?.ToUpper() + "%";
        return await PaginatedList<ProveedorDto>.CreateAsync(
            _context.ProveedorMateriales
            .Where(p => p.Status == "A" && (request.Nombre == null || EF.Functions.Like(p.Nombre, nombreLike))
                && (request.Nit == null || p.Nit == request.Nit)
                && (request.Telefono == null || p.Telefono == request.Telefono)
                && (request.Email == null || p.Email == request.Email))
            .OrderBy(p => p.Nombre)
            .ProjectTo<ProveedorDto>(_mapper.ConfigurationProvider)
            , request.PageNumber, request.PageSize);
    }
}
