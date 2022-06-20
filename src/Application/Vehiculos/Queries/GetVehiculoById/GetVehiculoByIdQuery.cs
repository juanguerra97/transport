using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Exceptions;
using seminario.Application.Common.Interfaces;
using seminario.Domain.Entities;

namespace seminario.Application.Vehiculos.Queries.GetVehiculoById;

public record GetVehiculoByIdQuery : IRequest<VehiculoDto>
{
    public int VehiculoId { get; set; }
}

public class GetVehiculoByIdQueryHandler : IRequestHandler<GetVehiculoByIdQuery, VehiculoDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetVehiculoByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<VehiculoDto> Handle(GetVehiculoByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Vehiculos
            .FirstOrDefaultAsync(v => v.Id == request.VehiculoId && v.Status != "X", cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Vehiculo), request.VehiculoId);
        }
        return _mapper.Map<Vehiculo, VehiculoDto>(entity);
    }
}