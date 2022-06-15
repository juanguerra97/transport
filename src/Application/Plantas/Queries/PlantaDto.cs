using AutoMapper;
using seminario.Application.Bodegas.Queries;
using seminario.Application.Common.Mappings;
using seminario.Application.TipoPlantas.Queries;
using seminario.Application.Usuarios.Queries;
using seminario.Domain.Entities;

namespace seminario.Application.Plantas.Queries;
public class PlantaDto : IMapFrom<Planta>
{
    public int? Id { get; set; }
    public TipoPlantaDto? TipoPlanta { get; set; }
    public string? Descripcion { get; set; }
    public string? Detalle { get; set; }
    public BodegaDto? Bodega { get; set; }

    public UsuarioDto? Encargado { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Planta, PlantaDto>()
            .ForMember(d => d.Encargado, opt => opt.MapFrom(s => s.AdminPlanta.User));
    }
}
