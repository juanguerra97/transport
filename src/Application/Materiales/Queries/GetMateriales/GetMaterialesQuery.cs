using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using seminario.Application.Common.Interfaces;
using seminario.Application.Common.Models;

namespace seminario.Application.Materiales.Queries.GetMateriales;
public record GetMaterialesQuery : IRequest<PaginatedList<MaterialDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int? TipoMaterialId { get; set; }

}
public class GetMaterialesQueryHandler : IRequestHandler<GetMaterialesQuery, PaginatedList<MaterialDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetMaterialesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<MaterialDto>> Handle(GetMaterialesQuery request, CancellationToken cancellationToken)
    {
        return await PaginatedList<MaterialDto>.CreateAsync(
            _context.Material
            .Where(m => m.Status == "A" && (request.TipoMaterialId == null || m.TipoMaterialId == request.TipoMaterialId))
            .OrderBy(m => m.Id)
            .ProjectTo<MaterialDto>(_mapper.ConfigurationProvider)
            , request.PageNumber, request.PageSize);
    }
}
