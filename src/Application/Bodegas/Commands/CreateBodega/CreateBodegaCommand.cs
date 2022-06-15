using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Interfaces;
using seminario.Domain.Entities;
using seminario.Domain.Enums;

namespace seminario.Application.Bodegas.Commands.CreateBodega;
public record CreateBodegaCommand : IRequest<int?>
{
    public TipoBodega TipoBodega { get; init; }
    public string? Descripcion { get; init; }
    public string? Detalle { get; init; }
    public int? MunicipioId { get; init; }
    public string? Direccion { get; init; }
    public string EncargadoId { get; set; }
}

public class CreateBodegaCommandHandler : IRequestHandler<CreateBodegaCommand, int?>
{
    private readonly IApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public CreateBodegaCommandHandler(IApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<int?> Handle(CreateBodegaCommand request, CancellationToken cancellationToken)
    {

        var user = await _context.ApplicationUsers
            .FirstOrDefaultAsync(u => u.Id == request.EncargadoId, cancellationToken);

        if (!(await _userManager.IsInRoleAsync(user, "AdminBodega")))
        {
            await _userManager.AddToRoleAsync(user, "AdminBodega");
        }

        var ubicacion = new Ubicacion
        {
            TipoUbicacion = TipoUbicacion.BODEGA,
            Descripcion = request.Descripcion,
            Detalle = request.Detalle,
            Direccion = request.Direccion,
            MunicipioId = request.MunicipioId,
        };
        _context.Ubicaciones.Add(ubicacion);

        var bodega = new Bodega
        {
            TipoBodega = request.TipoBodega,
            Descripcion = request.Descripcion,
            Detalle = request.Detalle,
            Ubicacion = ubicacion,
            AdminBodega = new AdminBodega
            {
                User = user
            }
        };
        _context.Bodegas.Add(bodega);

        await _context.SaveChangesAsync(cancellationToken);

        return bodega.Id;
    }
}