using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Interfaces;
using seminario.Domain.Entities;
using seminario.Domain.Enums;

namespace seminario.Application.Plantas.Commands.CreatePlanta;
public record CreatePlantaCommand : IRequest<int?>
{
    public int? TipoPlantaId { get; init; }
    public string? Descripcion { get; init; }
    public string? Detalle { get; init; }
    public int? MunicipioId { get; init; }
    public string? Direccion { get; init; }
    public string EncargadoId { get; set; }
}

public class CreatePlantaCommandHandler : IRequestHandler<CreatePlantaCommand, int?>
{
    private readonly IApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public CreatePlantaCommandHandler(IApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    public async Task<int?> Handle(CreatePlantaCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.ApplicationUsers
            .FirstOrDefaultAsync(u => u.Id == request.EncargadoId, cancellationToken);

        if (!(await _userManager.IsInRoleAsync(user, "AdminPlanta")))
        {
            await _userManager.AddToRoleAsync(user, "AdminPlanta");
        }
        

        var ubicacion = new Ubicacion
        {
            TipoUbicacion = TipoUbicacion.PLANTA,
            Descripcion = request.Descripcion,
            Detalle = request.Detalle,
            Direccion = request.Direccion,
            MunicipioId = request.MunicipioId,
        };
        _context.Ubicaciones.Add(ubicacion);

        var bodega = new Bodega
        {
            TipoBodega = TipoBodega.PLANTA,
            Descripcion = request.Descripcion,
            Detalle = request.Detalle,
            Ubicacion = ubicacion
        };
        _context.Bodegas.Add(bodega);

        var planta = new Planta
        {
            TipoPlantaId = request.TipoPlantaId,
            Descripcion = request.Descripcion,
            Detalle = request.Detalle,
            Bodega = bodega,
            AdminPlanta = new AdminPlanta
            {
                User = user
            }
        };
        _context.Plantas.Add(planta);

        await _context.SaveChangesAsync(cancellationToken);

        return planta.Id;
    }
}