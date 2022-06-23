using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Exceptions;
using seminario.Application.Common.Interfaces;
using seminario.Domain.Entities;

namespace seminario.Application.Proveedores.Queries.GetProveedorById;
public record GetProveedorByIdQuery : IRequest<ProveedorDto>
{
    public int ProveedorId { get; set; }
}

public class GetProveedorByIdQueryHandler : IRequestHandler<GetProveedorByIdQuery, ProveedorDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProveedorByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ProveedorDto> Handle(GetProveedorByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.ProveedorMaterial
            .FirstOrDefaultAsync(p => p.Id == request.ProveedorId && p.Status == "A");
        if (entity == null)
        {
            throw new NotFoundException(nameof(ProveedorMaterial), request.ProveedorId);
        }

        return _mapper.Map<ProveedorMaterial, ProveedorDto>(entity);
    }
}
