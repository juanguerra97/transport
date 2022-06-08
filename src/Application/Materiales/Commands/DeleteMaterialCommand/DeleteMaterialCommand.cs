using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Exceptions;
using seminario.Application.Common.Interfaces;
using seminario.Application.Materiales.Queries;
using seminario.Domain.Entities;

namespace seminario.Application.Materiales.Commands.DeleteMaterialCommand;
public record DeleteMaterialCommand : IRequest<MaterialDto>
{
    public int? MaterialId { get; init; }
}

public class DeleteMaterialCommandHandler : IRequestHandler<DeleteMaterialCommand, MaterialDto>
{
    private IApplicationDbContext _context;
    private IMapper _mapper;

    public DeleteMaterialCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<MaterialDto> Handle(DeleteMaterialCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Materiales
            .FirstOrDefaultAsync(m => m.Id == request.MaterialId && m.Status == "A", cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Material), request.MaterialId);
        }

        entity.Status = "X";

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<Material, MaterialDto>(entity);
    }
}