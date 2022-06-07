using seminario.Application.Common.Mappings;

namespace seminario.Application.UnidadesMedida.Queries;
public class UnidadMedidaDto : IMapFrom<UnidadMedidaDto>
{
    public int? Id { get; set; }

    public string? Descripcion { get; set; }

    public string? DescripcionPlural { get; set; }

    public string? DescripcionCorta { get; set; }
}
