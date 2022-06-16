using MediatR;
using seminario.Application.Common.Interfaces;
using seminario.Domain.Entities;

namespace seminario.Application.Proveedores.Commands.CreateProveedorCommand;
public record CreateProveedorCommand : IRequest<int?>
{
    public string? Nombre { get; init; }
    public string? Nit { get; init; }
    public string? Telefono { get; init; }
    public string? Email { get; init; }
    public string? Direccion { get; init; }
}

public class CreateProveedorCommandHandler : IRequestHandler<CreateProveedorCommand, int?>
{
    private readonly IApplicationDbContext _context;

    public CreateProveedorCommandHandler(IApplicationDbContext context)
    {
        _context = context; 
    }

    public async Task<int?> Handle(CreateProveedorCommand request, CancellationToken cancellationToken)
    {

        var entity = new ProveedorMaterial
        {
            Nombre = request.Nombre,
            Nit = request.Nit,
            Email = request.Email,
            Telefono = request.Telefono,
            Direccion = request.Direccion
        };

        await _context.ProveedorMateriales.AddAsync(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}