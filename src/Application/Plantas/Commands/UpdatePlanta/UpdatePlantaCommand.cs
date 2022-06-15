
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Exceptions;
using seminario.Application.Common.Interfaces;
using seminario.Application.Plantas.Queries;
using seminario.Domain.Entities;

namespace seminario.Application.Plantas.Commands.UpdatePlanta;
public record UpdatePlantaCommand : IRequest<PlantaDto>
{
    public int? PlantaId { get; set; }
    public int? TipoPlantaId { get; init; }
    public string? Descripcion { get; init; }
    public string? Detalle { get; init; }
    public int? MunicipioId { get; init; }
    public string? Direccion { get; init; }
    public string EncargadoId { get; init; }
}

public class UpdatePlantaCommandHandler : IRequestHandler<UpdatePlantaCommand, PlantaDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public UpdatePlantaCommandHandler(IApplicationDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<PlantaDto> Handle(UpdatePlantaCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Plantas
            .Include(p => p.AdminPlanta)
            .ThenInclude(ad => ad.User)
            .Include(p => p.TipoPlanta)
            .Include(p => p.Bodega)
            .ThenInclude(b => b.Ubicacion)
            .ThenInclude(u => u.Municipio)
            .ThenInclude(m => m.Departamento)
            .ThenInclude(d => d.Pais)
            .FirstOrDefaultAsync(p => p.Id == request.PlantaId, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Planta), request.PlantaId);
        }

        entity.TipoPlantaId = request.TipoPlantaId;

        entity.Descripcion = request.Descripcion;
        entity.Bodega.Descripcion = request.Descripcion;
        entity.Bodega.Ubicacion.Descripcion = request.Descripcion;

        entity.Detalle = request.Detalle;
        entity.Bodega.Detalle = request.Detalle;
        entity.Bodega.Ubicacion.Detalle = request.Detalle;

        entity.Bodega.Ubicacion.Direccion = request.Direccion;
        entity.Bodega.Ubicacion.MunicipioId = request.MunicipioId;

        if (request.EncargadoId != entity.AdminPlanta?.UserId)
        {
            var oldAdmin = entity.AdminPlanta;
            if ((await _context.AdminPlantas.CountAsync(ad => ad.Status == "A" && ad.UserId == oldAdmin.UserId, cancellationToken)) <= 1)
            {
                if ((await _userManager.IsInRoleAsync(oldAdmin.User, "AdminPlanta")))
                {
                    await _userManager.RemoveFromRoleAsync(oldAdmin.User, "AdminPlanta");
                }
            }

            var user = await _context.ApplicationUsers
                .FirstOrDefaultAsync(u => u.Id == request.EncargadoId, cancellationToken);

            if (!(await _userManager.IsInRoleAsync(user, "AdminPlanta")))
            {
                await _userManager.AddToRoleAsync(user, "AdminPlanta");
            }

            entity.AdminPlanta = new AdminPlanta
            {
                User = user
            };
        }

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<Planta, PlantaDto>(entity);
    }
}
