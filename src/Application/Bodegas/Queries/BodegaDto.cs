using AutoMapper;
using seminario.Application.Common.Mappings;
using seminario.Application.Ubicaciones.Queries;
using seminario.Application.Usuarios.Queries;
using seminario.Domain.Entities;
using seminario.Domain.Enums;

namespace seminario.Application.Bodegas.Queries;
public class BodegaDto : IMapFrom<Bodega>
{
    public int? Id { get; set; }
    public TipoBodega TipoBodega { get; set; }
    public string? Descripcion { get; set; }
    public string? Detalle { get; set; }
    public UbicacionDto? Ubicacion { get; set; }
    public UsuarioDto? Encargado { get; set; }


    public void Mapping(Profile profile)
    {
        profile.CreateMap<Bodega, BodegaDto>()
            .ForMember(d => d.Encargado, opt => opt.MapFrom(s => s.AdminBodega.User));
    }
}
