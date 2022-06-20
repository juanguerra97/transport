using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Exceptions;
using seminario.Application.Common.Interfaces;
using seminario.Application.Conductores.Queries;
using seminario.Domain.Entities;

namespace seminario.Application.Conductores.Commands.UpdateConductor;

public record UpdateConductorCommand : IRequest<ConductorDto>
{
    public int ConductorId { get; set; }
    public string? NoLicencia { get; init; }
}

public class UpdateConductorCommandHandler : IRequestHandler<UpdateConductorCommand, ConductorDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateConductorCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ConductorDto> Handle(UpdateConductorCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Conductores
            .Include(c => c.User)
            .FirstOrDefaultAsync(c => c.Id == request.ConductorId && c.Status != "X", cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Conductor), request.ConductorId);
        }

        entity.NoLicencia = request.NoLicencia;
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<Conductor, ConductorDto>(entity);
    }
}
