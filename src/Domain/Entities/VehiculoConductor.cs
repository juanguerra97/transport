
namespace seminario.Domain.Entities;
public class VehiculoConductor : AuditableEntity
{
    public int? VehiculoId { get; set; }

    public Vehiculo? Vehiculo { get; set; }

    public string? ConductorId { get; set; }

    public Conductor? Conductor { get; set; }

}