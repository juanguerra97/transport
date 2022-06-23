using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Exceptions;
using seminario.Application.Common.Interfaces;
using seminario.Application.Proveedores.Queries;
using seminario.Domain.Entities;

namespace seminario.Application.Proveedores.Commands.DeleteProveedorCommand;
public record DeleteProveedorCommand : IRequest<ProveedorDto>
{
    public int ProveedorId { get; init; }
}

public class DeleteProveedorCommandHandler : IRequestHandler<DeleteProveedorCommand, ProveedorDto>
{
    private IApplicationDbContext _context;
    private IMapper _mapper;

    public DeleteProveedorCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ProveedorDto> Handle(DeleteProveedorCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ProveedorMaterial
            .FirstOrDefaultAsync(p => p.Id == request.ProveedorId && p.Status == "A", cancellationToken);
        
        if(entity == null)
        {
            throw new NotFoundException(nameof(ProveedorMaterial), request.ProveedorId);
        }

        entity.Status = "X";
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ProveedorMaterial, ProveedorDto>(entity);
    }
}
