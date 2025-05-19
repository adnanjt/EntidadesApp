using System.ComponentModel.DataAnnotations;

namespace EntidadesApi.Domain.Entities
{
    public class TipoEntidad
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [StringLength(200)]
        public string Descripcion { get; set; }

        // Campos de auditoría
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public DateTime? FechaModificacion { get; set; }
    }
}