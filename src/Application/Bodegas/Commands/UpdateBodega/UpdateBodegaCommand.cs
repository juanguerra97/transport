using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Bodegas.Queries;
using seminario.Application.Common.Exceptions;
using seminario.Application.Common.Interfaces;
using seminario.Domain.Entities;
using seminario.Domain.Enums;

namespace seminario.Application.Bodegas.Commands.UpdateBodega;
public record UpdateBodegaCommand : IRequest<BodegaDto>
{
    public int? BodegaId { get; set; }
    public TipoBodega TipoBodega { get; init; }
    public string? Descripcion { get; init; }
    public string? Detalle { get; init; }
    public int? MunicipioId { get; init; }
    public string? Direccion { get; init; }
    public string EncargadoId { get; init; }
}

public class UpdateBodegaCommandHandler : IRequestHandler<UpdateBodegaCommand, BodegaDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public UpdateBodegaCommandHandler(IApplicationDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<BodegaDto> Handle(UpdateBodegaCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Bodega
            .Include(b => b.AdminBodega)
            .ThenInclude(ad => ad.User)
            .Include(b => b.Ubicacion)
            .ThenInclude(u => u.Municipio)
            .ThenInclude(m => m.Departamento)
            .ThenInclude(d => d.Pais)
            .FirstOrDefaultAsync(p => p.Id == request.BodegaId, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Bodega), request.BodegaId);
        }

        entity.TipoBodega = request.TipoBodega;

        entity.Descripcion = request.Descripcion;
        entity.Ubicacion.Descripcion = request.Descripcion;

        entity.Detalle = request.Detalle;
        entity.Ubicacion.Detalle = request.Detalle;

        entity.Ubicacion.Direccion = request.Direccion;
        entity.Ubicacion.MunicipioId = request.MunicipioId;

        if (request.EncargadoId != entity.AdminBodega?.UserId)
        {
            var oldAdmin = entity.AdminBodega;
            if ((await _context.AdminBodega.CountAsync(ad => ad.Status == "A" && ad.UserId == oldAdmin.UserId, cancellationToken)) <= 1)
            {
                if ((await _userManager.IsInRoleAsync(oldAdmin.User, "AdminBodega")))
                {
                    await _userManager.RemoveFromRoleAsync(oldAdmin.User, "AdminBodega");
                }
            }

            var user = await _context.ApplicationUser
                .FirstOrDefaultAsync(u => u.Id == request.EncargadoId, cancellationToken);

            if (!(await _userManager.IsInRoleAsync(user, "AdminBodega")))
            {
                await _userManager.AddToRoleAsync(user, "AdminBodega");
            }

            entity.AdminBodega = new AdminBodega
            {
                User = user
            };
        }

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<Bodega, BodegaDto>(entity);
    }
}