using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Exceptions;
using seminario.Application.Common.Interfaces;
using seminario.Application.Proveedores.Queries;
using seminario.Domain.Entities;

namespace seminario.Application.Proveedores.Commands.UpdateProveedorCommand;
public record UpdateProveedorCommand : IRequest<ProveedorDto>
{
    public int? ProveedorId { get; set; }
    public string? Nombre { get; init; }
    public string? Nit { get; init; }
    public string? Telefono { get; init; }
    public string? Email { get; init; }
    public string? Direccion { get; init; }
}

public class UpdateProveedorCommandHandler : IRequestHandler<UpdateProveedorCommand, ProveedorDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateProveedorCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ProveedorDto> Handle(UpdateProveedorCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ProveedorMaterial
            .FirstOrDefaultAsync(p => p.Id == request.ProveedorId && p.Status == "A", cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(ProveedorMaterial), request.ProveedorId);
        }

        entity.Nombre = request.Nombre;
        entity.Nit = request.Nit;
        entity.Email = request.Email;
        entity.Telefono = request.Telefono;
        entity.Direccion = request.Direccion;

        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<ProveedorMaterial, ProveedorDto>(entity);
    }
}