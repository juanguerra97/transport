using MediatR;
using seminario.Application.Common.Interfaces;
using seminario.Domain.Entities;

namespace seminario.Application.UnidadesMedida.Commands.CreateUnidadMedida;

public record CreateUnidadMedidaCommand : IRequest<int?>
{
    public string? Descripcion { get; init; }

    public string? DescripcionPlural { get; init; }

    public string? DescripcionCorta { get; init; }
}

public class CreateUnidadMedidaCommandHandler : IRequestHandler<CreateUnidadMedidaCommand, int?>
{
    private IApplicationDbContext _context;

    public CreateUnidadMedidaCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int?> Handle(CreateUnidadMedidaCommand request, CancellationToken cancellationToken)
    {
        var unidadMedida = new UnidadMedida
        {
            Descripcion = request.Descripcion,
            DescripcionCorta = request.DescripcionCorta,
            DescripcionPlural = request.DescripcionPlural,
        };

        _context.UnidadMedidas.Add(unidadMedida);
        await _context.SaveChangesAsync(cancellationToken);

        return unidadMedida.Id;
    }
}