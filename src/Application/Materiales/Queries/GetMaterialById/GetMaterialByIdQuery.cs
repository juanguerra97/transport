using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Exceptions;
using seminario.Application.Common.Interfaces;
using seminario.Domain.Entities;

namespace seminario.Application.Materiales.Queries.GetMaterialById;

public record GetMaterialByIdQuery : IRequest<MaterialDto>
{
    public int MaterialId { get; init; }
}

public class GetMaterialByIdQueryHandler : IRequestHandler<GetMaterialByIdQuery, MaterialDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetMaterialByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<MaterialDto> Handle(GetMaterialByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Material
            .Include(m => m.UnidadMedida)
            .Include(m => m.TipoMaterial)
            .FirstOrDefaultAsync(m => m.Id == request.MaterialId, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Material), request.MaterialId);
        }

        return _mapper.Map<Material, MaterialDto>(entity);
    }
}