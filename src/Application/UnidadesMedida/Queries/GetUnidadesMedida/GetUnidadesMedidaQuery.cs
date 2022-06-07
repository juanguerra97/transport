using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Interfaces;

namespace seminario.Application.UnidadesMedida.Queries.GetUnidadesMedida;
public record GetUnidadesMedidaQuery : IRequest<List<UnidadMedidaDto>>
{
}

public class GetUnidadesMedidaQueryHandler : IRequestHandler<GetUnidadesMedidaQuery, List<UnidadMedidaDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetUnidadesMedidaQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<UnidadMedidaDto>> Handle(GetUnidadesMedidaQuery request, CancellationToken cancellationToken)
    {
        return await _context.UnidadMedidas
            .Where(u => u.Status == "A")
            .OrderBy(u => u.Descripcion)
            .ThenBy(u => u.DescripcionCorta)
            .ProjectTo<UnidadMedidaDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
